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
        
        public User Buyer { get; set; }
        

        [ForeignKey("Buyer")]
        [Required(ErrorMessage = "Buyer is required field.")]
        public string BuyerId { get; set; }

        public Lot Lot { get; set; }

        [ForeignKey("Lot")]
        [Required(ErrorMessage = "Lot is required field.")]
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
