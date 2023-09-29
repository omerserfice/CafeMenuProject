using CafeMenuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CafeMenuProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly CafeMenuDbContext _context;

        public UserController(CafeMenuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
            
        }

        [HttpGet]
        public IActionResult AddUser()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddUser([FromForm] User user)
        {
            string password = user.Hashpassword;
            byte[] passwordsalt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(passwordsalt);
            }

            byte[] hashedPassword = HashPassword(password, passwordsalt);


            if (!string.IsNullOrEmpty(password))
             
                Redirect("/Admin/User/AddUser");


            //string hashedPassword = ComputeSha256Hash(password);

            var newUser = new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Hashpassword = Convert.ToBase64String(hashedPassword),
                Saltpassword = Convert.ToBase64String(passwordsalt)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            
            return Redirect("/Admin/User/Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Userid == id);
            if (user != null)
            
            return View(user);

            return View();
        }
        [HttpPost]
        public IActionResult Edit([FromForm] User user)
        {
           var userCurrent = _context.Users.FirstOrDefault(x => x.Userid == user.Userid);
           if(userCurrent != null)

               
            userCurrent.Name = user.Name;
            userCurrent.Surname = user.Surname;
            userCurrent.Username = user.Username;
            
            _context.Users.Update(userCurrent);
            _context.SaveChanges();
            return Redirect("/Admin/User/Edit/"+user.Userid);
        }

        public IActionResult DeleteUser(int id)
        {
            var userCurrent = _context.Users.FirstOrDefault(x => x.Userid == id);
            if (userCurrent != null)
                _context.Users.Remove(userCurrent);
                _context.SaveChanges();
                return Redirect("/Admin/User/Index");
        }


        public static byte[] HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32); // 32 bytes is a good size for a password hash
            }
        }

    }
}
