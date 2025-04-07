using AgencyPlatform.Application.DTOs.CuponesCliente;
using AgencyPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuponesClienteController : ControllerBase
    {
        private readonly ICuponesClienteService _service;

        public CuponesClienteController(ICuponesClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CuponClienteDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuponClienteDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CrearCuponClienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCuponClienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
