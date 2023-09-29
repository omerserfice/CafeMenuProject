using System;
using System.Collections.Generic;

namespace CafeMenuProject.Models
{
    public partial class Product
    {
        public Product()
        {
            Productproperties = new HashSet<Productproperty>();
        }

        public int Productid { get; set; }
        public string? Productname { get; set; }
        public int? Categoryid { get; set; }
        public decimal? Price { get; set; }
        public string? Imagepath { get; set; }
        public bool? Isdeleted { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Creatoruserid { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User? Creatoruser { get; set; }
        public virtual ICollection<Productproperty> Productproperties { get; set; }
    }
}
