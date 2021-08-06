using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Entity.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
