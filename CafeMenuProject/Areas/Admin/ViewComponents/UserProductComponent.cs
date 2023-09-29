using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeMenuProject.Areas.Admin.ViewComponents
{
	public class UserProductComponent : ViewComponent
	{
		private readonly CafeMenuDbContext _context;

		public UserProductComponent(CafeMenuDbContext context)
		{
			_context = context;
		}


		public IViewComponentResult Invoke(int id)
		{
			// View Component içinde verileri işleyebilirsiniz
			//var fullName = $"{firstName} {lastName}";

			var products = _context.Products.Where(p=>p.Creatoruserid == id)
				.Include(c=>c.Category)
				.Include(u => u.Creatoruser)
				.ToList();

			// İlgili View Component View'ini döndürün
			return View(products);
		}

	}
}
