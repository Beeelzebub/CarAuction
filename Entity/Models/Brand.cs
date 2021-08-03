using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        public string BrandName { get; set; }
    }
}
