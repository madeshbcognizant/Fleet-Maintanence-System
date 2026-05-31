using UserService.Models;

namespace UserService.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        string GenerateJwtToken(User user);
        bool ValidateRole(string role);
        Task<bool> CheckEmployeeIdExistsAsync(string employeeId);
    }
}
