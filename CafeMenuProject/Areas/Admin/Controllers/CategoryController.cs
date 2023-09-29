using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeMenuProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CafeMenuDbContext _context;

        public CategoryController(CafeMenuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.Include(c => c.Creatoruser).ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var users = _context.Users.ToList();
            ViewBag.Users = users;  
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory([FromForm] Category category)
        {
            var newCategory = new Category()
            {
                Categoryname = category.Categoryname,
                Parentcategoryid = 1,
                Isdeleted = false,
                Createddate = DateTime.Now,
                Creatoruserid = category.Creatoruserid
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();


            return Redirect("/Admin/Category/Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var users = _context.Users.ToList();
            ViewBag.Users = users;
            var category = _context.Categories.FirstOrDefault(x => x.Categoryid == id);
            if (category != null)

                return View(category);

            return View();
        }
        [HttpPost]
        public IActionResult Edit([FromForm] Category category)
        {
            var categoryCurrent = _context.Categories.FirstOrDefault(x => x.Categoryid == category.Categoryid);
            if (categoryCurrent != null)

            categoryCurrent.Categoryname = category.Categoryname;
            categoryCurrent.Creatoruserid = category.Creatoruserid;
            
            _context.Categories.Update(categoryCurrent);
            _context.SaveChanges();
            return Redirect("/Admin/Category/Edit/" + category.Categoryid);
        }

        public IActionResult DeleteCategory(int id)
        {
            var categoryCurrent = _context.Categories.FirstOrDefault(x => x.Categoryid == id);
            if (categoryCurrent != null)
                _context.Categories.Remove(categoryCurrent);
            _context.SaveChanges();
            return Redirect("/Admin/Category/Index");
        }

    }
}
