using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Bit
    {
        [Key]
        public Guid Id { get; set; }
        
        public User Buyer { get; set; }

        public Lot Lot { get; set; }
    }
}
