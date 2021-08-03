using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Lot
    {
        [Key]
        public int Id { get; set; }

        public string FuelType { get; set; }
    }
}
