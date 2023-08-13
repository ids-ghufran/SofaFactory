using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using SofaFactory.Data;
using System.Data.SqlTypes;
using SofaFactory.Models;
using SofaFactory.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SofaFactory.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        // GET: Products
        public async Task<IActionResult> Index(int page=1,int pageLength=10,bool isJson=false, string? Materials=null,string? Sizes=null, string? StorageTypes = null, string? SeatingCapacities = null, string? query=null)
        {
            var skip= (page-1<1?0:page-1)*pageLength;
            var materials = Materials!=null? Uri.UnescapeDataString( Materials).Split(",").ToList():new List<string>();
            var sizes = Sizes!=null? Uri.UnescapeDataString(Sizes).Split(",").ToList(): new List<string>();
            var storageTypes = StorageTypes != null ? Uri.UnescapeDataString(StorageTypes).Split(",").ToList() : new List<string>();
            var sc = SeatingCapacities != null ? Uri.UnescapeDataString(SeatingCapacities).Split(",").ToList() : new List<string>();
            ViewBag.Materials = materials;
            ViewBag.Sizes = sizes;
            ViewBag.StorageTypes = storageTypes;
            ViewBag.SeatingCapacities = sc;
            ViewBag.Method = "Index";
            SetViewBag();
            var applicationDbContext = _context.Products.AsQueryable();
            if (query != null)
            {
                //query = query.Replace("+", " ");
                applicationDbContext = queryProduct(applicationDbContext, query);
            }
            applicationDbContext = FilterQuery(applicationDbContext, materials, sizes, storageTypes, sc);
            
            applicationDbContext = applicationDbContext.Include(p=>p.ProductImages).ThenInclude(p=>p.Image)
                .Include(p => p.Category).Include(p => p.CreatedBy)
                .Include(p => p.SubCategory).Include(p => p.UpdatedBy);
           
            var count = applicationDbContext.Count();
            var pagemodel = new PaginationModel<Product>() { 
            Model=await Paginate(applicationDbContext, page,pageLength).ToListAsync(),
            Page=page,
            PageLength=pageLength,
            Count=count
            };
            if (!isJson)
                return View(pagemodel);
            else
            {
                HttpContext.Response.StatusCode = 200;
                return Json(pagemodel);
            }
        }
        [Route("/Products/category/{category}")]
        public async Task<IActionResult> GetByCategory(int page = 1, int pageLength = 10, bool isJson = false, string? Materials = null, string? Sizes = null, string? StorageTypes = null, string? SeatingCapacities = null, string? query = null,string? category=null)
        {
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            category=Uri.UnescapeDataString(category);
            var materials = Materials != null ? Uri.UnescapeDataString(Materials).Split(",").ToList() : new List<string>();
            var sizes = Sizes != null ? Uri.UnescapeDataString(Sizes).Split(",").ToList() : new List<string>();
            var storageTypes = StorageTypes != null ? Uri.UnescapeDataString(StorageTypes).Split(",").ToList() : new List<string>();
            var sc = SeatingCapacities != null ? Uri.UnescapeDataString(SeatingCapacities).Split(",").ToList() : new List<string>();
            ViewBag.Materials = materials;
            ViewBag.Sizes = sizes;
            ViewBag.StorageTypes = storageTypes;
            ViewBag.SeatingCapacities = sc;
            ViewBag.Method = "Index";
            var applicationDbContext = _context.Products.Where(c=>c.Category.Name==category).AsQueryable();
            if (query != null)
            {
                //query = query.Replace("+", " ");
                applicationDbContext = queryProduct(applicationDbContext, query);
            }
            applicationDbContext = FilterQuery(applicationDbContext, materials, sizes, storageTypes, sc);
            applicationDbContext = applicationDbContext.Include(p => p.ProductImages).ThenInclude(p => p.Image)
                .Include(p => p.Category).Include(p => p.CreatedBy)
                .Include(p => p.SubCategory).Include(p => p.UpdatedBy);
            var count = applicationDbContext.Count();
            SetViewBag();
            var pagemodel = new PaginationModel<Product>()
            {
                Model = await Paginate(applicationDbContext,page,pageLength)   .ToListAsync(),
                Page = page,
                PageLength = pageLength,
                Count = count
            };
            if (!isJson)
                return View("Index",pagemodel);
            else
                return Json(pagemodel);
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages).ThenInclude(p => p.Image)
               .Include(p => p.CreatedBy)
                .Include(p => p.SubCategory).Include(p => p.UpdatedBy)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById");
            //ViewData["SubCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById");
            ViewData["CategoryId"] = await _context.Categories.ToListAsync();
            ViewData["SizeId"] = await _context.Sizes.ToListAsync();
            ViewData["MaterialId"]=await _context.Materials.ToListAsync();
            ViewData["StorageTypeId"] = await _context.StorageTypes.ToListAsync();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductVm modal)
        {
            if (modal.Images==null|| modal.Images.Count < 1)
            {
                Response.StatusCode = 422;
                ModelState.AddModelError("Images", "Please upload atleast one product image.");
                return Json(ModelState.ToSerializedDictionary());
            }
            var product = new Product
            {
                Name = modal.Name,
                Description = modal.Description,
                Details = modal.Details,
                CategoryId = modal.CategoryId,
                SubCategoryId = modal.SubCategoryId,
                Discount = modal.Discount,
                Emi=modal.Emi,
                Price=modal.Price,
                DiscountType = modal.DiscountType,
                Rating = modal.Rating,
                Quantity = modal.Quantity,
                Dimensions = modal.Dimensions,
                Highlights = modal.Highlights,
                Color = modal.Color,
                Warranty = modal.Warranty,
                SeatingCapacityId = modal.SeatingCapacityId,
                SizeId=modal.SizeId,
                StorageTypeId=modal.StorageTypeId,
                MaterialId=modal.MaterialId,
                AssemblyDetails = modal.AssemblyDetails,
                PackageDetails = modal.PackageDetails,
                CreatedOn = DateTime.Now
            };
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 422;
                return Json(ModelState.ToSerializedDictionary());
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            var imgPath = Path.Combine(environment.WebRootPath, "assets", "images", "products");
            if (modal.Images.Count >0)
            {
                var imgUrl = await FileHelper.SaveFilesAsync(modal.Images, imgPath);
                var imgs = new List<ProductImage>();
                for(var i = 0; i<modal.Images.Count; i++)
                {

                    imgs.Add(new ProductImage
                    {
                        Image = new Image
                        {
                            Src = imgUrl[0],
                            Alt = "Product Image",
                            CreatedOn = DateTime.Now
                        },
                        ProductId = product.ProductId,
                        Rank = i+1
                    });
                }
                _context.ProductImages.AddRange(imgs);
                await _context.SaveChangesAsync();
            }
            return Ok("Success");
            
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.CategoryId);
            //ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.CreatedById);
            //ViewData["SubCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.SubCategoryId);
            //ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.UpdatedById);
            
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.CreatedById);
            ViewData["SubCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.SubCategoryId);
            ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.UpdatedById);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Details,CategoryId,SubCategoryId,Discount,DiscountType,Rating,Quantity,Dimensions,Highlights,Color,Warranty,SeatingCapacity,AssemblyDetails,PackageDetails,CreatedOn,UpdatedOn,CreatedById,UpdatedById")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.CategoryId);
            ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.CreatedById);
            ViewData["SubCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.SubCategoryId);
            ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.UpdatedById);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.CreatedBy)
                .Include(p => p.SubCategory)
                .Include(p => p.UpdatedBy)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        private IQueryable<Product> Paginate(IQueryable<Product> product,  int page, int pageSize)
        {
            var skip = (page - 1 < 1 ? 0 : page - 1) * pageSize;
            return product.Skip(skip).Take(pageSize);
        }
        private IQueryable<Product> FilterQuery(IQueryable<Product> applicationDbContext, IList<string> Materials, IList<string> Sizes, IList<string> StorageTypes, IList<string> SeatingCapacities)
        {
           
            if (Materials != null && Materials.Count > 0)
            {
                applicationDbContext = applicationDbContext.Where(c => Materials.Contains(c.Material.Label));
            }
            if (Sizes != null && Sizes.Count > 0)
            {
                applicationDbContext = applicationDbContext.Where(c => Sizes.Contains(c.Size.Label));
            }
            if (StorageTypes != null && StorageTypes.Count > 0)
            {
                applicationDbContext = applicationDbContext.Where(c => StorageTypes.Contains(c.StorageType.Label));
            }
            if (SeatingCapacities != null && SeatingCapacities.Count > 0)
            {
                applicationDbContext = applicationDbContext.Where(c => SeatingCapacities.Contains(c.SeatingCapacity.Label));
            }
            return applicationDbContext;
        }
        private IQueryable<Product> queryProduct( IQueryable<Product> applicationDbContext, string?query)
        {
            if (query != null)
            {
                query = Uri.UnescapeDataString(query);
                applicationDbContext = applicationDbContext.Where(x => x.Name.Contains(query));
            }
            return applicationDbContext;
        }
        private void SetViewBag()
        {
            
            ViewBag.QueryString = HttpContext.Request.QueryString.Value;
            ViewBag.filters = new Filters()
            {
                Materials = _context.Materials.Select(x => x.Label).ToList(),
                SeatingCapacities = _context.SeatingCapacities.Select(x => x.Label).ToList(),
                Sizes = _context.Sizes.Select(c => c.Label).ToList(),
                StorageTypes = _context.StorageTypes.Select(x => x.Label).ToList(),
            };
        }
    }
}
