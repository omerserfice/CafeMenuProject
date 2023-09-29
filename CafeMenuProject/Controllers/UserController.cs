using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeMenuProject.Controllers
{
	public class UserController : Controller
	{

		private readonly CafeMenuDbContext _context;

        public UserController(CafeMenuDbContext context)
        {
            _context = context;
        }

        public IActionResult UserList(int? id)
		{
			Main();
			var users = _context.Users.ToList();
			ViewBag.Comp = id;
			return View(users);
		}

		static async Task Main()
		{
			using (HttpClient client = new HttpClient())
			{
				string apiURL = "";
                HttpResponseMessage response = await client.GetAsync(apiURL);
				if (response.IsSuccessStatusCode) {
				string content = await response.Content.ReadAsStringAsync();


				}

            }
		}
	}
}
