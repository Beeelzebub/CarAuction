using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class DriveUnit
    {
        [Key]
        public Guid Id { get; set; }

        public string DriveUnitType { get; set; }
    }
}
