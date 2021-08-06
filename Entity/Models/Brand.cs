using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<Model> Models { get; set; } 
        public virtual ICollection<Car> Cars { get; set; }
    }
}
