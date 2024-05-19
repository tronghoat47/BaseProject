using System.ComponentModel.DataAnnotations;

namespace BaseProject.Application.Models.Requests
{
    public class UserLoginRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}