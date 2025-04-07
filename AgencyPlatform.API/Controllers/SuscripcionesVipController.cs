using AgencyPlatform.Application.DTOs.SuscripcionesVip;
using AgencyPlatform.Application.Interfaces.Services.SuscripcionesVip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcionesVipController : ControllerBase
    {
        private readonly ISuscripcionVipService _service;

        public SuscripcionesVipController(ISuscripcionVipService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var result = await _service.ObtenerTodasAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _service.ObtenerPorIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Suscripción no encontrada." });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearSuscripcionVipDto dto)
        {
            var result = await _service.CrearAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UpdateSuscripcionVipDto dto)
        {
            var result = await _service.ActualizarAsync(id, dto);
            if (result == null)
                return NotFound(new { message = "Suscripción no encontrada para actualizar." });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var success = await _service.EliminarAsync(id);
            if (!success)
                return NotFound(new { message = "Suscripción no encontrada para eliminar." });

            return Ok(new { message = "Suscripción eliminada correctamente." });
        }
    }
}
