using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SofaFactory.Data;
using Domain.Models;
using SofaFactory.Helper;

namespace SofaFactory.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImageController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImageController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // POST: ImageController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile File)
        {
            if(File == null||File.Length<1)
            {
                Response.StatusCode = 422;
                return Json(new { error = "please upload valid file" });
            }
            var userid = _context.AppUsers.Where(v => v.UserName == User.Identity.Name).Select(v => v.Id).FirstOrDefault();
            var image = new Image() {
                Alt = Path.GetFileNameWithoutExtension(File.Name),
                CreatedById = userid == null ? "" : userid,
                UpdatedById = userid,
                CreatedOn= DateTime.Now,
                UpdatedOn=DateTime.Now
            };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", DateTime.Now.ToString("MM-yyyy"));
            var filepath=FileHelper.SaveFileAsync(File, path);
            image.Src = await filepath;
            _context.Images.Add(image);
            var id = _context.SaveChanges();
            return Json(image);
        }

     
       

        // POST: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
