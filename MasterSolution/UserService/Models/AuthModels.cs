using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } 
    }

    public class LoginRequest
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string EmployeeId { get; set; }
        public string Role { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
