using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeMenuProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly CafeMenuDbContext _context;

        public ProductController(CafeMenuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(c => c.Category)
                .Include(c=>c.Creatoruser)
                .ToList();
            return View(products);
       
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var users = _context.Users.ToList();
            ViewBag.Users = users;
            var category = _context.Categories.ToList();
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct([FromForm] Product product, IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);
            var newimagename = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", newimagename);
            var stream = new FileStream(location, FileMode.Create);

            image.CopyTo(stream);
            stream.Close();
            var newProduct = new Product()
            {
                Imagepath = newimagename,
                Productname = product.Productname,
                Categoryid = product.Categoryid,
                Price = product.Price,
                Createddate = DateTime.Now,
                Creatoruserid = product.Creatoruserid
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();


            return Redirect("/Admin/Product/Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var users = _context.Users.ToList();
            ViewBag.Users = users;
            var category = _context.Categories.ToList();
            ViewBag.Category = category;
            var product= _context.Products.FirstOrDefault(x => x.Productid == id);
            if (product != null)

                return View(product);

            return View();
        }
        [HttpPost]
        public IActionResult Edit([FromForm] Product product,IFormFile image)
        {
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", product.Imagepath);
            if (image != null)
            
               
                if (System.IO.File.Exists(location))
                
                    System.IO.File.Delete(location);
                    var extension = Path.GetExtension(image.FileName);
                    var newimagename = Guid.NewGuid() + extension;
                     var lc = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", newimagename);
                    var stream = new FileStream(lc, FileMode.Create);

                    image.CopyTo(stream);
                    stream.Close();



            var productCurrent = _context.Products.FirstOrDefault(x => x.Productid == product.Productid);
            if (productCurrent != null)

            productCurrent.Productname = product.Productname;
            productCurrent.Categoryid = product.Categoryid;
            productCurrent.Createddate = DateTime.Now;
            productCurrent.Creatoruserid = product.Creatoruserid;
            productCurrent.Price = product.Price;
            productCurrent.Isdeleted = false;
            productCurrent.Imagepath = newimagename;



            _context.Products.Update(productCurrent);
            _context.SaveChanges();
            return Redirect("/Admin/Product/Edit/" + product.Productid);
        }

        public IActionResult DeleteProduct(int id)
        {
            var productCurrent = _context.Products.FirstOrDefault(x => x.Productid == id);
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/", productCurrent.Imagepath);
            if (productCurrent != null)
                if (System.IO.File.Exists(location))

                    System.IO.File.Delete(location);

            _context.Products.Remove(productCurrent);
            _context.SaveChanges();
            return Redirect("/Admin/Product/Index");
        }


    }
}
