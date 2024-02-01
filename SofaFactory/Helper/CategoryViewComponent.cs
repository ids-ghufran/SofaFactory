using Microsoft.AspNetCore.Mvc;
using SofaFactory.Data;

namespace SofaFactory.Helper
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CategoryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.Categories.Where(x => x.ParentId == null).ToList();
            return View(items);
        }

    }
}
