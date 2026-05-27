using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;
using UserService.Repository;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            // Validate role
            if (!ValidateRole(request.Role))
            {
                return null;
            }

            // Check if employee ID already exists
            if (await _userRepository.EmployeeIdExistsAsync(request.EmployeeId))
            {
                return null;
            }

            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create user
            var user = new User
            {
                EmployeeId = request.EmployeeId,
                PasswordHash = passwordHash,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            // Generate token
            var token = GenerateJwtToken(createdUser);
            var expiresAt = DateTime.UtcNow.AddHours(
                double.Parse(_configuration["Jwt:ExpiryHours"] ?? "24"));

            return new AuthResponse
            {
                Token = token,
                EmployeeId = createdUser.EmployeeId,
                Role = createdUser.Role,
                ExpiresAt = expiresAt
            };
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            // Get user by employee ID
            var user = await _userRepository.GetUserByEmployeeIdAsync(request.EmployeeId);

            if (user == null)
            {
                return null;
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            // Update last login
            await _userRepository.UpdateLastLoginAsync(user.UserId);

            // Generate token
            var token = GenerateJwtToken(user);
            var expiresAt = DateTime.UtcNow.AddHours(
                double.Parse(_configuration["Jwt:ExpiryHours"] ?? "24"));

            return new AuthResponse
            {
                Token = token,
                EmployeeId = user.EmployeeId,
                Role = user.Role,
                ExpiresAt = expiresAt
            };
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.EmployeeId),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("EmployeeId", user.EmployeeId)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(
                    double.Parse(_configuration["Jwt:ExpiryHours"] ?? "24")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateRole(string role)
        {
            var validRoles = new[] { "Manager", "Technician", "Viewer" };
            return validRoles.Contains(role, StringComparer.OrdinalIgnoreCase);
        }

        public async Task<bool> CheckEmployeeIdExistsAsync(string employeeId)
        {
            return await _userRepository.EmployeeIdExistsAsync(employeeId);
        }
    }
}
