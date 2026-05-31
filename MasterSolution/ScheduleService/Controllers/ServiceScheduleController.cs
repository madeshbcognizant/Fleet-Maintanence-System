using Microsoft.AspNetCore.Mvc;
using ScheduleService.DTOs; // Please add this DTO for accessing
using ScheduleService.Models;
using ScheduleService.Services;
 
namespace ScheduleService.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ServiceScheduleController : ControllerBase
    {
        private readonly ServiceScheduleService _scheduleService;

        public ServiceScheduleController(ServiceScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        // GET: api/ServiceSchedule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceSchedule>>> GetSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        // GET: api/ServiceSchedule/{ServiceId}
        [HttpGet("{ServiceId}")]
        public async Task<ActionResult<ServiceSchedule>> GetSchedule(string ServiceId)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(ServiceId);

            if (schedule == null)
            {
                return NotFound(new { message = $"Schedule with ID '{ServiceId}' not found." });
            }

            return Ok(schedule);
        }

        // POST: api/ServiceSchedule
        [HttpPost]
        public async Task<ActionResult<ServiceSchedule>> PostSchedule(ServiceSchedule schedule)
        {
            if (schedule == null)
            {
                return BadRequest();
            }

            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule);

            if (createdSchedule == null)
            {
                return Conflict(new { message = $"A schedule with ID '{schedule.ServiceId}' already exists." });
            }

            // FIXED: Key changed from 'id' to 'ServiceId' to match the GetSchedule parameter
            return CreatedAtAction(nameof(GetSchedule), new { ServiceId = createdSchedule.ServiceId }, createdSchedule);
        }

        // ==========================================
        // NEW ENDPOINTS TO SUPPORT INTER-SERVICE FLOWS
        // ==========================================

        // POST: api/ServiceSchedule/trigger
        // Automatically fetches from Vehicle, Master, and Odometer services
        [HttpPost("trigger")]
        public async Task<ActionResult<ServiceSchedule>> TriggerSchedule([FromQuery] string regId, [FromQuery] string type, [FromQuery] string serviceName)
        {
            var schedule = await _scheduleService.TriggerAndGenerateScheduleAsync(regId, type, serviceName);
            if (schedule == null)
            {
                return BadRequest("Unable to generate schedule. Please check external microservice dependencies.");
            }
            return Ok(schedule);
        }

        // GET: api/ServiceSchedule/technicians
        // Populates the fleet manager's assignment modal with available staff
        [HttpGet("technicians")]
        public async Task<ActionResult<IEnumerable<TechnicianDTO>>> GetAvailableTechnicians()
        {
            var technicians = await _scheduleService.GetTechniciansForDropdownAsync();
            return Ok(technicians);
        }

        // POST: api/ServiceSchedule/assign
        // Submits the finalized work details straight to ScheduleAssignmentService
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTechnician([FromBody] AssignmentRequest request)
        {
            var success = await _scheduleService.AssignTechnicianAsync(request.ServiceId, request.TechnicianId);
            if (!success)
            {
                return BadRequest("Assignment failed. Verify connection to Technician service or check ServiceId.");
            }
            return Ok(new { message = "Work successfully assigned to technician." });
        }
    }
}