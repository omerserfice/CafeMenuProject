using System;
using System.Collections.Generic;

namespace CafeMenuProject.Models
{
    public partial class Productproperty
    {
        public Productproperty()
        {
            Categories = new HashSet<Category>();
        }

        public int Productpropertyid { get; set; }
        public int? Productid { get; set; }
        public int? Propertyid { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Property? Property { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
