using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public class Lot
    {
        public int Id { get; set; }
        
        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; } 
        
        public decimal MinimalStep { get; set; }
        
        public decimal StartingPrice { get; set; }
        
        public decimal CurrentCost { get; set; }
        public decimal RedemptionPrice { get; set; }
        
        public virtual Car Car { get; set; }

        public User Seller { get; set; } 
        public string SellerId { get; set; } 
        public LotStatus Status { get; set; }


        public ICollection<Bid> Bids { get; set; } = new List<Bid>();

    }

    public enum LotStatus
    {
        Pending,
        Approved,
        Denied,
        Ended
    }
}
