using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Models
{
    public class AuthUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}