using AgencyPlatform.Application.DTOs.Cupones;
using AgencyPlatform.Application.Interfaces.Services.Cupones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuponesController : ControllerBase
{
    private readonly ICuponService _cuponService;

    public CuponesController(ICuponService cuponService)
    {
        _cuponService = cuponService;
    }

    /// <summary>
    /// Obtener cupones activos disponibles.
    /// </summary>
    [HttpGet]
    [AllowAnonymous] // o puedes autorizar con roles si quieres limitar
    public async Task<ActionResult<List<CuponDto>>> GetDisponibles()
    {
        var cupones = await _cuponService.ObtenerDisponiblesAsync();
        return Ok(cupones);
    }

    /// <summary>
    /// Obtener un cupón por código.
    /// </summary>
    [HttpGet("codigo/{codigo}")]
    [AllowAnonymous]
    public async Task<ActionResult<CuponDto>> GetPorCodigo(string codigo)
    {
        var cupon = await _cuponService.ObtenerPorCodigoAsync(codigo);
        if (cupon == null)
            return NotFound(new { message = "Cupón no encontrado" });

        return Ok(cupon);
    }

    /// <summary>
    /// Crear un nuevo cupón (admin).
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "admin,sistema")]
    public async Task<IActionResult> Crear([FromBody] CrearCuponDto dto)
    {
        await _cuponService.CrearAsync(dto);
        return Ok(new { message = "Cupón creado correctamente" });
    }

    /// <summary>
    /// Canjear un cupón con puntos.
    /// </summary>
    [HttpPost("canjear")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Canjear([FromBody] CanjearCuponDto dto)
    {
        var result = await _cuponService.CanjearCuponAsync(dto);
        if (!result)
            return BadRequest(new { message = "No se puede canjear el cupón" });

        return Ok(new { message = "Cupón canjeado correctamente" });
    }
}
