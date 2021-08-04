using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Model
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Brand Brand { get; set; }
    }
}
