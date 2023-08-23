using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using SofaFactory.Data;
using Microsoft.AspNetCore.Http;
using SofaFactory.Models;
using Microsoft.AspNetCore.Authorization;
using SofaFactory.Helper;

namespace SofaFactory.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        [Authorize(Roles =$"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Index(int? categoryId, int page=1, int pageLength=8, string? search= null)
        {
            var skip= (page - 1)*pageLength;
            Category? parent = null;
            var applicationDbContext = _context.Categories.AsQueryable();

            if(categoryId == null)
            {
                applicationDbContext = applicationDbContext.Where(x => x.ParentId == null);
            }
            else
            {
                parent = await applicationDbContext.Where(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
                
                applicationDbContext = applicationDbContext.Where(x => x.ParentId == categoryId);
            }

            if (search != null)
            {
                applicationDbContext= applicationDbContext.Where(c=>c.Name.Contains(search)||c.Description.Contains(search)).AsQueryable();
            }
            var models = await applicationDbContext.Select(c=>new Category
            {
                Name=c.Name, Description=c.Description,
                CategoryId=c.CategoryId,
                Image= _context.Images.Where(p=>p.ImageId==c.ImageId).FirstOrDefault()
            })
                .Skip(skip).Take(pageLength).ToListAsync();
            ViewBag.Count = _context.Categories.Count(); 
            ViewBag.PageLength = pageLength;
            ViewBag.Page = page;
            ViewBag.search= search;
            ViewBag.Parent = parent;
            
            return View(models);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        [Route("Categories/get-subcategories/{categoryId}")]
        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCat = await _context.Categories.Where(x => x.ParentId == categoryId).ToListAsync();
            return Json(subCat);
        }

        // GET: Categories/Details/5
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.CreatedBy)
                .Include(c => c.Image)
                .Include(c => c.UpdatedBy)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public IActionResult Create(int? categoryId)
        {
            //ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            //ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "Alt");
            //ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            ViewBag.CategoryId = categoryId;
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Create(CategoryVm category)
        {
            if (ModelState.IsValid && !(category.Image == null || category.Image.Length == 0))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "category");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
               var imageLoc=  FileHelper.SaveFileAsync(category.Image, path);
               
                var cat = new Category()
                {
                    Name = category.Name,
                    Description = category.Description,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    ParentId = category.ParentId,
                    Image = new Domain.Models.Image()
                    {
                        Alt = category.Name,
                        Src = await imageLoc
                    },

                };
                _context.Categories.Add(cat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (category.Image == null||category.Image.Length==0)
            {
                ModelState.AddModelError("Image", "Please Upload valid image file.");
            }
            //ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", category.CreatedById);
            //ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "Alt", category.ImageId);
            //ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", category.UpdatedById);
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.Include(c=>c.Image).Where(c=>c.CategoryId==id).FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound();
            }
            if (category.ParentId != null)
            {
                ViewBag.Parent= _context.Categories.Where(c=>c.CategoryId== category.ParentId).FirstOrDefault();
            }
            //ViewData["CreatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", category.CreatedById);
            //ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "Alt", category.ImageId);
            //ViewData["UpdatedById"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", category.UpdatedById);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Admin},{Roles.SubAdmin}")]
        public async Task<IActionResult> Edit(int id, CategoryVm category)
        {
            var appUser= _context.AppUsers.Where(c=>c.UserName== User.Identity.Name).FirstOrDefault();
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var cat = _context.Categories.Include(c => c.Image).Where(x => x.CategoryId == id).FirstOrDefault();
                cat.Name = category.Name;
                cat.UpdatedOn = DateTime.Now;
                cat.Description=category.Description;
                cat.UpdatedById = appUser.Id;
                if(category.Image!= null&& category.Image.Length>0) {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "category");
                    var fname = "";//filename
                    var delPath = Path.Combine(path, Path.GetFileName( cat.Image.Src));
                    System.IO.File.Delete(delPath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fname = category.Image.FileName;
                    path= Path.Combine(path, fname);
                    using(var fs= System.IO.File.Create(path))
                    {                        
                        category.Image.CopyTo(fs);
                    }
                    cat.Image.Src ="/"+ Path.GetRelativePath("wwwRoot",path).Replace("\\","/");
                }
                try
                {
                    //_context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            
            
            
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.CreatedBy)
                .Include(c => c.Image)
                .Include(c => c.UpdatedBy)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            var children= await _context.Categories.Where(m=>m.ParentId==id).ToListAsync();
            foreach(var child in children)
            {
                child.ParentId = null;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
