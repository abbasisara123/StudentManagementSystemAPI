using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.DTOs.User
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
