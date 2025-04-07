// 📁 Ruta: API/Controllers/MembresiasVipController.cs

using AgencyPlatform.Application.DTOs.MembresiasVip;
using AgencyPlatform.Application.Interfaces.Services.MembresiasVip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiasVipController : ControllerBase
    {
        private readonly IMembresiaVipService _service;

        public MembresiasVipController(IMembresiaVipService service)
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
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearMembresiaVipDto dto)
        {
            var result = await _service.CrearAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UpdateMembresiaVipDto dto)
        {
            var result = await _service.ActualizarAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var success = await _service.EliminarAsync(id);
            if (!success)
                return NotFound(new { message = "No se encontró la membresía VIP." });

            return Ok(new { message = "Membresía VIP eliminada correctamente." });
        }
    }
}
