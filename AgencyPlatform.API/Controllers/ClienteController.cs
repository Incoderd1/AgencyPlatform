using AgencyPlatform.Application.DTOs.Clientes;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Cliente/paginado?page=1&pageSize=10
        [HttpGet("paginado")]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<ClienteResumenDto>>> GetPaginado(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var paginacion = await _clienteService.GetPaginatedAsync(page, pageSize);
            return Ok(paginacion);
        }

        // GET: api/Cliente (todos)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ClienteResumenDto>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            return Ok(cliente);
        }

        // GET: api/Cliente/5/resumen
        [HttpGet("{id}/resumen")]
        [Authorize]
        public async Task<ActionResult<ClienteResumenDto>> GetResumen(int id)
        {
            var cliente = await _clienteService.GetResumenByIdAsync(id);
            return Ok(cliente);
        }

        // POST: api/Cliente/8 (idUsuario)
        [HttpPost("{idUsuario}")]
        [Authorize]
        public async Task<ActionResult<ClienteDto>> Create(int idUsuario, [FromBody] CrearClienteDto dto)
        {
            var result = await _clienteService.CreateAsync(idUsuario, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdCliente }, result);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ClienteDto>> Update(int id, [FromBody] UpdateClienteDto dto)
        {
            var result = await _clienteService.UpdateAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _clienteService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
