using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using SofaFactory.Data;
using SofaFactory.Models;
using SofaFactory.Helper;

namespace SofaFactory.Controllers
{
    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;

        public SliderController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        // GET: Slider
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Slider.Include(s => s.Image);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Slider/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Slider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderVm modal)
        {
            if (!ModelState.IsValid)
                return View(modal);
            var imgPath = Path.Combine(environment.WebRootPath, "assets", "images", "slider");
            try
            {
                var imgUrl = await FileHelper.SaveFileAsync(modal.Image, imgPath);
                var slider = new Slider
                {
                    Title = modal.Title,
                    Description = modal.Description,
                    BtnLink = modal.BtnLink,
                    Image = new Image
                    {
                        Alt = !string.IsNullOrEmpty(modal.Title) ? modal.Title:"Slider Image",
                        Src = imgUrl
                    }
                };
                await _context.AddAsync(slider);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Slider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.Include(x => x.Image).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (slider == null)
            {
                return NotFound();
            }
            var sliderVm = new SliderVm
            {
                Id = slider.Id,
                BtnLink = slider.BtnLink,
                Description = slider.Description,
                Title = slider.Title
            };
            return View(sliderVm);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderVm modal)
        {

                var slider = await _context.Slider.Include(x => x.Image).Where(x => x.Id == modal.Id).FirstOrDefaultAsync();
                var oldImg = slider.Image;
                slider.BtnLink = modal.BtnLink;
                slider.Description = modal.Description;
                slider.Title = modal.Title;
                if(modal.Image != null)
                {
                var imgPath = Path.Combine(environment.WebRootPath, "assets", "images", "slider");
                var imgUrl = await FileHelper.SaveFileAsync(modal.Image, imgPath);
                slider.Image = new Image
                {
                    Alt = !string.IsNullOrEmpty(modal.Title) ? modal.Title : "Slider Image",
                    Src = imgUrl
                };

                };
                try
                {
                if(modal.Image != null)
                {
                    _context.Images.Remove(oldImg);
                    FileHelper.DeleteFile(Path.Combine(environment.WebRootPath, oldImg.Src));
                }
                    
                await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!SliderExists(slider.Id))
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

        // GET: Slider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Slider == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
            }
            var imgUrl = "";
            var slider = await _context.Slider.Include(x => x.Image).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (slider != null)
            {
                imgUrl = slider.Image.Src;
                _context.Images.Remove(slider.Image);
                _context.Slider.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            FileHelper.DeleteFile(Path.Combine(environment.WebRootPath, imgUrl));
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return (_context.Slider?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
