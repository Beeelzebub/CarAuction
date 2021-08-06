using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Model
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public  Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}