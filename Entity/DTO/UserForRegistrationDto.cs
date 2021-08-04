using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Entity.DTO
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(15, ErrorMessage = "Max length 15 symbols")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(15, ErrorMessage = "Max length 15 symbols"), MinLength(4, ErrorMessage = "Min length 4 symbols")]

        public string Password { get; set; }
        public string Role { get; set; }
    }
}
