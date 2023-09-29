using System;
using System.Collections.Generic;

namespace CafeMenuProject.Models
{
    public partial class Property
    {
        public Property()
        {
            Productproperties = new HashSet<Productproperty>();
        }

        public int Propertyid { get; set; }
        public int? Key { get; set; }
        public string? Value { get; set; }

        public virtual ICollection<Productproperty> Productproperties { get; set; }
    }
}
