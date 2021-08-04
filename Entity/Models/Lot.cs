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

        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; }

        [NotMapped]
        public TimeSpan LeftTime { get; set; }

        public int BidsCount { get; set; }

        [Column(TypeName = "money")]
        public decimal MinimalStep { get; set; }

        [Column(TypeName = "money")]
        public decimal CurrentCost { get; set; }

        public Car Car { get; set; }
        public Guid CarID { get; set; }

        public User Seller { get; set; }
        public Guid SellerId { get; set; }

        [ForeignKey("LastBit")]
        public Guid LastBitId { get; set; }
        public Bid LastBit { get; set; }

    }
}
