using AgencyPlatform.Application.DTOs.Perfil;
using AgencyPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgencyPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilesController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // Obtener todos los perfiles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var perfiles = await _perfilService.GetAllAsync();
            return Ok(perfiles); // Devuelve la lista de perfiles
        }

        // Obtener un perfil por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var perfil = await _perfilService.GetByIdAsync(id);
            if (perfil == null)
                return NotFound(); // Si no se encuentra el perfil, devuelve 404
            return Ok(perfil); // Devuelve el perfil si existe
        }
        // Obtener el detalle del perfil con galería de imágenes
        [HttpGet("{id}/detalle")]
        [AllowAnonymous] // Opcional, útil si quieres que esté accesible sin autenticación
        public async Task<IActionResult> GetDetalleById(int id)
        {
            var perfil = await _perfilService.GetDetalleByIdAsync(id);
            if (perfil == null)
                return NotFound(); // Si no se encuentra, devuelve 404
            return Ok(perfil); // Devuelve el perfil detallado con galería
        }


        // Crear un nuevo perfil
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearPerfilDto dto)
        {
            try
            {
                var perfil = await _perfilService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = perfil.Id }, perfil);
                // Retorna 201 con la URL del nuevo perfil
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // Si la validación falla, devuelve 400
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el perfil.", details = ex.Message });
                // Si ocurre un error inesperado, devuelve 500
            }
        }

        // Actualizar un perfil existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePerfilDto dto)
        {
            var perfil = await _perfilService.UpdateAsync(id, dto);
            if (perfil == null)
                return NotFound(); // Si no se encuentra el perfil, devuelve 404
            return Ok(perfil); // Devuelve el perfil actualizado
        }

        // Eliminar un perfil por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _perfilService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(); // Si el perfil no se encuentra, devuelve 404
            return NoContent(); // Devuelve 204 si el perfil fue eliminado correctamente
        }
    }
}
