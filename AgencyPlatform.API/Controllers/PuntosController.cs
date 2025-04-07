using AgencyPlatform.Application.DTOs.Puntos;
using AgencyPlatform.Application.Interfaces.Services.Puntos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuntosController : ControllerBase
    {
        private readonly IPuntoService _puntoService;

        public PuntosController(IPuntoService puntoService)
        {
            _puntoService = puntoService;
        }

        /// <summary>
        /// Crear un nuevo registro de puntos para un cliente.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "admin,soporte,sistema")] // puedes ajustar los roles
        public async Task<IActionResult> Crear([FromBody] CrearPuntoDto dto)
        {
            await _puntoService.CrearAsync(dto);
            return Ok(new { message = "Punto registrado correctamente." });
        }

        /// <summary>
        /// Obtener el historial de puntos de un cliente.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "cliente,admin,sistema")]
        public async Task<ActionResult<List<PuntoDto>>> ObtenerPorCliente([FromQuery] int clienteId)
        {
            var puntos = await _puntoService.ObtenerPorClienteAsync(clienteId);
            return Ok(puntos);
        }

        /// <summary>
        /// Obtener resumen de puntos acumulados, usados y expirados.
        /// </summary>
        [HttpGet("resumen")]
        [Authorize(Roles = "cliente,admin,sistema")]
        public async Task<ActionResult<ResumenPuntosDto>> ObtenerResumen([FromQuery] int clienteId)
        {
            var resumen = await _puntoService.ObtenerResumenAsync(clienteId);
            return Ok(resumen);
        }
    }

}
