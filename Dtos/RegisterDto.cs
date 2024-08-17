using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password should be at least 6 characters long", MinimumLength = 6)]
        public required string Password { get; set; }
    }

}
