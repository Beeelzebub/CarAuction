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

        [Column(TypeName = "money")]
        public decimal StartingPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal MinimalStep { get; set; }

        [Column(TypeName = "money")]
        public decimal CurrentCost { get; set; }

        public Car Car { get; set; }
        public Guid CarId { get; set; }

        public User Seller { get; set; }
        public string SellerId { get; set; }


        public List<Bid> Bids { get; set; } = new List<Bid>();

    }
}
