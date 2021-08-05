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

        [Required(ErrorMessage = "Buyer is required field.")]
        public User Buyer { get; set; }
        
        public string BuyerId { get; set; }

        [Required(ErrorMessage = "Lot is required field.")]
        public Lot Lot { get; set; }
        
        public Guid LotId { get; set; }

        [Required(ErrorMessage = "Bid Status is required field.")]
        public BidStatus BidStatus { get; set; }
    }

    public enum BidStatus
    {
        Active,
        Outbid,
        Won
    }
}
