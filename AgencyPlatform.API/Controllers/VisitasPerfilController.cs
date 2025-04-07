using AgencyPlatform.Application.DTOs.VisitasPerfil;
using AgencyPlatform.Application.Interfaces.Services.VisitasPerfil;
using AgencyPlatform.Infrastructure.Services.VisitasPerfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class VisitasPerfilController : ControllerBase
{
    private readonly IVisitasPerfilService _service;

    public VisitasPerfilController(IVisitasPerfilService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<VisitaPerfilDto>>> GetAll()
    {
        var visitas = await _service.ObtenerTodasAsync();
        return Ok(visitas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VisitaPerfilDto>> GetById(int id)
    {
        var visita = await _service.ObtenerPorIdAsync(id);
        if (visita == null) return NotFound();
        return Ok(visita);
    }

    [HttpPost]
    public async Task<ActionResult<VisitaPerfilDto>> Create([FromBody] CrearVisitaPerfilDto dto)
    {
        var nuevaVisita = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = nuevaVisita.IdVisita }, nuevaVisita);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.EliminarAsync(id);
        return eliminado ? NoContent() : NotFound();
    }
    [HttpGet("contar/{idPerfil}")]
    public async Task<IActionResult> ContarVisitasPorPerfil(int idPerfil)
    {
        var cantidadVisitas = await _service.ContarVisitasPorPerfil(idPerfil);
        return Ok(new { CantidadVisitas = cantidadVisitas });
    }

}