using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(15, ErrorMessage = "Max length 15 symbols")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(15, ErrorMessage = "Max length 15 symbols"), MinLength(4, ErrorMessage = "Min length 4 symbols")]

        public string Password { get; set; }
    }
}
