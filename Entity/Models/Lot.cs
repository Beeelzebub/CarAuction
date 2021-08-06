using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Lot
    {
        public Guid Id { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public decimal MinimalStep { get; set; }
        
        public decimal StartingPrice { get; set; }
        
        public decimal CurrentCost { get; set; }
        
        public virtual Car Car { get; set; }
        public Guid CarId { get; set; }
        
        public virtual User Seller { get; set; }
        public string SellerId { get; set; }


        public virtual ICollection<Bid> Bids { get; set; }

    }
}
