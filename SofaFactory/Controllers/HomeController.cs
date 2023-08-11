using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SofaFactory.Data;
using SofaFactory.Models;
using System.Diagnostics;

namespace SofaFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Slider = await context.Slider.Include(x => x.Image).ToListAsync();
            var cats = await context.Categories.Include(x => x.Image).Take(7).ToListAsync();
            return View(cats);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}