using System;
using System.Collections.Generic;

namespace CafeMenuProject.Models
{
    public partial class User
    {
        public User()
        {
            Categories = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int Userid { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Hashpassword { get; set; }
        public string? Saltpassword { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
