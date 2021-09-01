using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entity.Models
{
    public class User : IdentityUser, IEntity
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        public  ICollection<Lot> Lots { get; set; }
        public  ICollection<Bid> Bids { get; set; }
    }
}
