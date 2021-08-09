using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public  ICollection<Model> Models { get; set; } 
        public  ICollection<Car> Cars { get; set; }
    }
}
