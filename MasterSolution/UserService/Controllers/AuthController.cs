using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // POST: api/Auth/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate role first
            if (!_authService.ValidateRole(request.Role))
            {
                return BadRequest($"Invalid role '{request.Role}'. Valid roles are: Manager, Technician, Viewer");
            }

            // Check if employee ID exists
            var employeeExists = await ((UserService.Services.AuthService)_authService).CheckEmployeeIdExistsAsync(request.EmployeeId);
            if (employeeExists)
            {
                return BadRequest($"Employee ID '{request.EmployeeId}' already exists. Please use a different employee ID.");
            }

            var response = await _authService.RegisterAsync(request);

            if (response == null)
            {
                return BadRequest("Registration failed. Please try again.");
            }

            return Ok(response);
        }

        // POST: api/Auth/login
        [HttpPost("login")]

        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized("Invalid employee ID or password.");
            }

            return Ok(response);
        }
    }
}
