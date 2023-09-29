using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

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
				string apiURL = "http://www.tcmb.gov.tr/kurlar/today.xml";
                HttpResponseMessage response = await client.GetAsync(apiURL);
				if (response.IsSuccessStatusCode) {
				string content = await response.Content.ReadAsStringAsync();

                    XmlSerializer serializer = new XmlSerializer(typeof(Currency));
                    using (StringReader reader = new StringReader(content))
                    {
                        Currency cr = (Currency)serializer.Deserialize(reader);
                       
                       Console.WriteLine($"Author: {cr.CurrencyName}");
						
                      
                    }


                }

            }
		}
	}
}
