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
        
        [Required(ErrorMessage = "Start Date is a required field.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is a required field.")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Minimal Step is a required field.")]
        public decimal MinimalStep { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Starting Price is a required field.")]
        public decimal StartingPrice { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Current Cost is a required field.")]
        public decimal CurrentCost { get; set; }

        [Required(ErrorMessage = "Car is a required field.")]
        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public Guid CarId { get; set; }

        [Required(ErrorMessage = "Seller is a required field.")]
        public User Seller { get; set; }
        public string SellerId { get; set; }


        public List<Bid> Bids { get; set; } = new List<Bid>();

    }
}
