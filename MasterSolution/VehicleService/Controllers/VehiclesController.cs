using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleService.Models;
using VehicleService.Services;

namespace VehicleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesService _vehiclesService;

        public VehiclesController(VehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicles()
        {
            var vehicles = await _vehiclesService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        // GET: api/Vehicles/VH-1234
        [HttpGet("{registrationId}")]
        public async Task<ActionResult<Vehicle>> GetVehicleByRegistrationId(string registrationId)
        {
            var vehicle = await _vehiclesService.GetVehicleByRegistrationIdAsync(registrationId);

            if (vehicle == null)
            {
                return NotFound($"Vehicle with Registration ID '{registrationId}' not found.");
            }

            return Ok(vehicle);
        }

        // GET: api/Vehicles/chasis/ABC12345678901234
        [HttpGet("chasis/{chasisNumber}")]
        public async Task<ActionResult<Vehicle>> GetVehicleByChasisNumber(string chasisNumber)
        {
            var vehicle = await _vehiclesService.GetVehicleByChasisNumberAsync(chasisNumber);

            if (vehicle == null)
            {
                return NotFound($"Vehicle with chasis number '{chasisNumber}' not found.");
            }

            return Ok(vehicle);
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> CreateVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdVehicle = await _vehiclesService.CreateVehicleAsync(vehicle);

            if (createdVehicle == null)
            {
                return BadRequest("Failed to create vehicle. Registration ID or Chasis Number may already exist.");
            }

            return CreatedAtAction(nameof(GetVehicleByRegistrationId), new { registrationId = createdVehicle.RegistrationId }, createdVehicle);
        }

        // PUT: api/Vehicles/VH-1234
        [HttpPut("{registrationId}")]
        public async Task<IActionResult> UpdateVehicle(string registrationId, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedVehicle = await _vehiclesService.UpdateVehicleAsync(registrationId, vehicle);

            if (updatedVehicle == null)
            {
                return NotFound($"Vehicle with Registration ID '{registrationId}' not found or update failed.");
            }

            return Ok(updatedVehicle);
        }

        // DELETE: api/Vehicles/VH-1234
        [HttpDelete("{registrationId}")]
        public async Task<IActionResult> DeleteVehicle(string registrationId)
        {
            var result = await _vehiclesService.DeleteVehicleAsync(registrationId);

            if (!result)
            {
                return NotFound($"Vehicle with Registration ID '{registrationId}' not found.");
            }

            return NoContent();
        }

        // GET: api/Vehicles/exists/VH-1234
        [HttpGet("exists/{registrationId}")]
        public async Task<ActionResult<bool>> RegistrationIdExists(string registrationId)
        {
            var exists = await _vehiclesService.RegistrationIdExistsAsync(registrationId);
            return Ok(new { registrationId, exists });
        }

        // GET: api/Vehicles/chasis-exists/ABC12345678901234
        [HttpGet("chasis-exists/{chasisNumber}")]
        public async Task<ActionResult<bool>> ChasisNumberExists(string chasisNumber)
        {
            var exists = await _vehiclesService.ChasisNumberExistsAsync(chasisNumber);
            return Ok(new { chasisNumber, exists });
        }
    }
}
