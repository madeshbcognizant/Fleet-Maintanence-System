using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterService.Models;
using MasterService.Services;

namespace MasterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly Services.MasterService _masterService;

        // Controller now talks exclusively to the Service layer
        public MasterController(Services.MasterService masterService)
        {
            _masterService = masterService;
        }

        // GET: api/Master
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Master>>> GetMasters()
        {
            var masters = await _masterService.GetAllMastersAsync();
            return Ok(masters);
        }

        // GET: api/Master/{type}
        [HttpGet("{type}")]
        public async Task<ActionResult<Master>> GetMaster(string type)
        {
            var master = await _masterService.GetMasterByTypeAsync(type);
            if (master == null)
            {
                return NotFound(new { Message = $"Master with type '{type}' not found" });
            }
            return Ok(master);
        }

        // POST: api/Master
        [HttpPost]
        public async Task<ActionResult<Master>> PostMaster(Master master)
        {
            if (master == null)
            {
                return BadRequest();
            }

            try
            {
                var createdMaster = await _masterService.CreateMasterAsync(master);

                if (createdMaster == null)
                {
                    return Conflict(new { Message = $"A master record with Type '{master.Type}' already exists" });
                }

                // FIXED: Now correctly points to GetMaster (singular) with the route parameter 'type'
                return CreatedAtAction(nameof(GetMaster), new { type = createdMaster.Type }, createdMaster);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "An error occurred during database save operation.");
            }
        }
    }
}