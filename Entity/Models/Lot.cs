using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Lot
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? StartDate { get; set; } 

        public DateTime EndDate { get; set; }

        [NotMapped]
        public TimeSpan LeftTime { get; set; }

        public int BitsCount { get; set; }

        [Column(TypeName = "money")]
        public decimal MinimalBit { get; set; }

        [Column(TypeName = "money")]
        public decimal CurrentCost { get; set; }

        public User Seller { get; set; }

    }
}
