using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicianService.models;
using TechnicianService.Repository;

namespace TechnicianService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechniciansController : ControllerBase
    {
        private readonly TechnicianRepository _repository;

        public TechniciansController(TechnicianRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technician>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Technician>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Technician>> Create(Technician technician)
        {
            var created = await _repository.AddAsync(technician);
            return CreatedAtAction(nameof(GetById), new { id = created.TechnicianId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Technician technician)
        {
            if (id != technician.TechnicianId) return BadRequest();

            var updated = await _repository.UpdateAsync(technician);
            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
