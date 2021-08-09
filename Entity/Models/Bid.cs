using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class Bid
    {
        public int Id { get; set; }
        
        public BidStatus BidStatus { get; set; }
        public  Lot Lot { get; set; }
        
        public int LotId { get; set; }
        
        public  User Buyer { get; set; }

        public string BuyerId { get; set; }
    }

    public enum BidStatus
    {
        Active,
        Outbid,
        Won
    }
}
