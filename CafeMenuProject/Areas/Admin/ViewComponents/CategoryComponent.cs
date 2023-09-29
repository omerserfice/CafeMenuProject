using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeMenuProject.Areas.Admin.ViewComponents
{
    public class CategoryComponent : ViewComponent
    {
        private readonly CafeMenuDbContext _context;

        public CategoryComponent(CafeMenuDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
			var categoryCounts = _context.Categories.Include(p=>p.Products).ToList();
			return View(categoryCounts);
        }
    }
}
