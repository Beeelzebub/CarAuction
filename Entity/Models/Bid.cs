using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Bid
    {
        [Key]
        public Guid Id { get; set; }
        
        public BidStatus BidStatus { get; set; }
        public virtual Lot Lot { get; set; }
        
        public Guid LotId { get; set; }
        
        public virtual User Buyer { get; set; }

        public string BuyerId { get; set; }
    }

    public enum BidStatus
    {
        Active,
        Outbid,
        Won
    }
}
