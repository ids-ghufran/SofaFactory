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
using Microsoft.AspNetCore.Authorization;
using Nelibur.ObjectMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                .Include(p => p.Category).Include(p => p.CreatedBy).Include(c=>c.Shape)
                .Include(p => p.SubCategory).Include(p => p.UpdatedBy);
            var count = applicationDbContext.Count();
            SetViewBag();
            ViewBag.BreadcrumbTitle = category;
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
               .Include(p => p.CreatedBy).Include(c=>c.Brand).Include(c=>c.Material).Include(c=>c.SeatingCapacity)
               .Include(v=>v.StorageType).Include(c=>c.Size).Include(c=>c.Shape)
                .Include(p => p.SubCategory).Include(p => p.UpdatedBy)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles=$"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById");
            //ViewData["SubCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById");
            ViewData["CategoryId"] = await _context.Categories.Where(x => x.ParentId == null).ToListAsync();
            ViewData["SizeId"] = await _context.Sizes.ToListAsync();
            ViewData["MaterialId"]=await _context.Materials.ToListAsync();
            ViewData["StorageTypeId"] = await _context.StorageTypes.ToListAsync();
            ViewData["SeatingCapacity"] = await _context.SeatingCapacities.ToListAsync();
            ViewData["Brands"] = await _context.Brands.ToListAsync();
            ViewData["Shapes"] = await _context.Shapes.ToListAsync();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Create([FromForm] ProductVm modal)
        {
            if (modal.ImageIds==null|| modal.ImageIds.Count < 1)
            {
                Response.StatusCode = 422;
                ModelState.AddModelError("Images", "Please upload atleast one product image.");
                return Json(ModelState.ToSerializedDictionary());
            }
            var user = _context.AppUsers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
            var product = new Product();
            TinyMapper.Bind<ProductVm, Product>();
            product = TinyMapper.Map<Product>(modal);
            product.UpdatedOn = DateTime.Now;
            product.CreatedOn = DateTime.Now;
            product.CreatedBy = user;
            product.UpdatedBy = user;
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 422;
                return Json(ModelState.ToSerializedDictionary());
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            var imgPath = Path.Combine(environment.WebRootPath, "assets", "images", "products");
            if (modal.ImageIds.Count >0)
            {
                await SaveProductImage(modal, product.ProductId);
            }
            return Ok("Success");
            
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.CategoryId);
            //ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.CreatedById);
            //ViewData["SubCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CreatedById", product.SubCategoryId);
            //ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", product.UpdatedById);
            
        }

        // GET: Products/Edit/5
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(c=>c.ProductImages).ThenInclude(p=>p.Image).Where(c=>c.ProductId==id).FirstAsync();
            if (product == null)
            {
                return NotFound();
            }
            await SetEditViewData(product);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Edit(int id,  ProductVm pm)
        {
            if (id != pm.ProductId)
            {
                return NotFound();
            }

            var based = await _context.Products.AsNoTracking().Where(c => c.ProductId == id).Include(l => l.ProductImages).ThenInclude(l => l.Image).FirstOrDefaultAsync();
            var product = new Product();
            if (ModelState.IsValid)
            {
                try
                {
                    TinyMapper.Bind<ProductVm, Product>();
                     product = TinyMapper.Map<Product>(pm);
                    _context.Entry(product).State = EntityState.Modified;
                    product.UpdatedOn=DateTime.Now;
                    product.CreatedOn = based.CreatedOn;
                    product.CreatedById=based.CreatedById;
                    product.UpdatedBy = _context.AppUsers.Where(c=>c.UserName== User.Identity.Name).FirstOrDefault();
                   // _context.Update(product);
                    await _context.SaveChangesAsync();
                    _context.SaveChanges();
                    if (pm.ImageIds.Count > 0)
                    {
                        var prodImg = _context.ProductImages.Where(c => c.ProductId == product.ProductId).ToList();
                        _context.ProductImages.RemoveRange(prodImg);
                        await SaveProductImage(pm, based.ProductId);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(pm.ProductId))
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
            await SetEditViewData(product);
            return View(based);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(c=>c.ProductImages)
                .ThenInclude(m=>m.Image)
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
        [Authorize(Roles = Roles.Admin)]
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
        private async Task SetEditViewData(Product product)
        {
            ViewData["CategoryId"] = await _context.Categories.Where(x => x.ParentId == null).ToListAsync();
            ViewData["SubCategoryId"] = await _context.Categories.Where(x => x.ParentId == product.CategoryId).ToListAsync();
            ViewData["SizeId"] = await _context.Sizes.ToListAsync();
            ViewData["MaterialId"] = await _context.Materials.ToListAsync();
            ViewData["StorageTypeId"] = await _context.StorageTypes.ToListAsync();
            ViewData["SeatingCapacity"] = await _context.SeatingCapacities.ToListAsync();
            ViewData["Brands"] = await _context.Brands.ToListAsync();
            ViewData["Shapes"] = await _context.Shapes.ToListAsync();
        }
        private async Task SaveProductImage(ProductVm pm,int productId)
        {
           var prodImg = new List<ProductImage>();
            foreach (var im in pm.ImageIds)
            {
                var p = new ProductImage()
                {
                    ProductId = productId,
                    ImageId = im,
                };
                prodImg.Add(p);
            }
            _context.AddRange(prodImg);
            _context.SaveChanges();
        }
    }
}
