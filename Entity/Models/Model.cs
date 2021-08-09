using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public  ICollection<Car> Cars { get; set; }
    }
}