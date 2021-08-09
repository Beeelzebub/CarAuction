using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Lot
    {
        public int Id { get; set; }
        
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        
        public decimal MinimalStep { get; set; }
        
        public decimal StartingPrice { get; set; }
        
        public decimal CurrentCost { get; set; }
        public decimal RedemptionPrice { get; set; }
        
        public virtual Car Car { get; set; }
        // public Guid CarId { get; set; }
        
        public  User Seller { get; set; }
        public string SellerId { get; set; }


        public  ICollection<Bid> Bids { get; set; }

    }
}
