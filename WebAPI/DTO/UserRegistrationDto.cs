using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
    }
}
