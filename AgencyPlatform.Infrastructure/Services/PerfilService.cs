using AgencyPlatform.Application.DTOs.Perfil;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Application.Validators;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AgencyPlatform.Infrastructure.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task<IEnumerable<PerfilDto>> GetAllAsync()
        {
            var perfiles = await _perfilRepository.Query()
        .Include(p => p.ImagenesPerfils)
        .Where(p => p.Estado == "activo") // solo los activos
        .ToListAsync();

            return perfiles.Select(p =>
            {
                var dto = MapToDto(p);

                // Buscar imagen principal
                var img = p.ImagenesPerfils
                    .Where(i => i.EsPrincipal && i.Estado == "aprobado")
                    .OrderBy(i => i.Orden)
                    .FirstOrDefault();

                dto.ImagenPrincipal = img?.UrlImagen;
                return dto;
            }).ToList();
        }

        public async Task<PerfilDto?> GetByIdAsync(int id)
        {
            var perfil = await _perfilRepository.Query().FirstOrDefaultAsync(p => p.IdPerfil == id);
            return perfil is null ? null : MapToDto(perfil);
        }

        public async Task<PerfilDto> CreateAsync(CrearPerfilDto dto)
        {
            var validator = new CrearPerfilDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Errores de validación: {errorMessages}");
            }

            var perfil = new Perfile
            {
                IdUsuario = dto.IdUsuario,
                IdAgencia = dto.IdAgencia,
                NombrePerfil = dto.NombrePerfil,
                Genero = dto.Genero,
                Edad = (short)(dto.Edad ?? 18),
                Altura = dto.Altura,
                Peso = dto.Peso,
                Medidas = dto.Medidas,
                ColorOjos = dto.ColorOjos,
                ColorCabello = dto.ColorCabello,
                Nacionalidad = dto.Nacionalidad,
                Descripcion = dto.Descripcion,
                UbicacionCiudad = dto.UbicacionCiudad,
                UbicacionZona = dto.UbicacionZona,
                DisponiblePara = dto.DisponiblePara ?? "todos",
                Disponible24h = dto.Disponible24h ?? false,
                DisponeLocal = dto.DisponeLocal ?? false,
                HaceSalidas = dto.HaceSalidas ?? false,
                EsIndependiente = dto.EsIndependiente ?? false,
                TelefonoContacto = dto.TelefonoContacto,
                Whatsapp = dto.Whatsapp,
                EmailContacto = dto.EmailContacto,
                FechaRegistro = DateTime.Now,
                FechaActualizacion = DateTime.Now,
                Estado = "pendiente",
                Destacado = false,
                NivelPopularidad = "bajo"
            };

            // Manejar los campos JSONB
            if (dto.Idiomas != null)
                perfil.Idiomas = JsonSerializer.Serialize(dto.Idiomas);

            if (dto.Servicios != null)
                perfil.Servicios = JsonSerializer.Serialize(dto.Servicios);

            if (dto.Tarifas != null)
                perfil.Tarifas = JsonSerializer.Serialize(dto.Tarifas);

            if (dto.Disponibilidad != null)
                perfil.Disponibilidad = JsonSerializer.Serialize(dto.Disponibilidad);

            try
            {
                await _perfilRepository.AddAsync(perfil);
                await _perfilRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Error al guardar el perfil: " + ex.InnerException?.Message ?? ex.Message);
            }

            return MapToDto(perfil);
        }
        public async Task<PerfilDetalleDto?> GetDetalleByIdAsync(int id)
        {
            var perfil = await _perfilRepository.Query()
                .Include(p => p.ImagenesPerfils)
                .FirstOrDefaultAsync(p => p.IdPerfil == id);

            if (perfil == null) return null;

            return new PerfilDetalleDto
            {
                IdPerfil = perfil.IdPerfil,
                NombrePerfil = perfil.NombrePerfil,
                Descripcion = perfil.Descripcion,
                TelefonoContacto = perfil.TelefonoContacto,
                Whatsapp = perfil.Whatsapp,
                GaleriaImagenes = perfil.ImagenesPerfils
                    .Where(i => i.Estado == "aprobado")
                    .OrderBy(i => i.Orden)
                    .Select(i => i.UrlImagen)
                    .ToList()
            };
        }


        public async Task<PerfilDto?> UpdateAsync(int id, UpdatePerfilDto dto)
        {
            var perfil = await _perfilRepository.Query().FirstOrDefaultAsync(p => p.IdPerfil == id);
            if (perfil == null) return null;

            perfil.NombrePerfil = dto.NombrePerfil ?? perfil.NombrePerfil;
            perfil.Genero = dto.Genero ?? perfil.Genero;
            perfil.Edad = (short)(dto.Edad ?? perfil.Edad);
            perfil.Altura = dto.Altura ?? perfil.Altura;
            perfil.Peso = dto.Peso ?? perfil.Peso;
            perfil.Medidas = dto.Medidas ?? perfil.Medidas;
            perfil.ColorOjos = dto.ColorOjos ?? perfil.ColorOjos;
            perfil.ColorCabello = dto.ColorCabello ?? perfil.ColorCabello;
            perfil.Nacionalidad = dto.Nacionalidad ?? perfil.Nacionalidad;

            if (dto.Idiomas is not null)
                perfil.Idiomas = JsonSerializer.Serialize(dto.Idiomas);

            if (dto.Descripcion is string descripcionStr)
                perfil.Descripcion = descripcionStr;

            if (dto.Servicios is not null)
                perfil.Servicios = JsonSerializer.Serialize(dto.Servicios);

            if (dto.Tarifas is not null)
                perfil.Tarifas = JsonSerializer.Serialize(dto.Tarifas);

            perfil.UbicacionCiudad = dto.UbicacionCiudad ?? perfil.UbicacionCiudad;
            perfil.UbicacionZona = dto.UbicacionZona ?? perfil.UbicacionZona;

            if (dto.Disponibilidad is not null)
                perfil.Disponibilidad = JsonSerializer.Serialize(dto.Disponibilidad);

            perfil.DisponiblePara = dto.DisponiblePara ?? perfil.DisponiblePara;
            perfil.Disponible24h = dto.Disponible24h ?? perfil.Disponible24h;
            perfil.DisponeLocal = dto.DisponeLocal ?? perfil.DisponeLocal;
            perfil.HaceSalidas = dto.HaceSalidas ?? perfil.HaceSalidas;
            perfil.TelefonoContacto = dto.TelefonoContacto ?? perfil.TelefonoContacto;
            perfil.Whatsapp = dto.Whatsapp ?? perfil.Whatsapp;
            perfil.EmailContacto = dto.EmailContacto ?? perfil.EmailContacto;

            perfil.Verificado = dto.Verificado ?? perfil.Verificado;
            perfil.FechaVerificacion = dto.FechaVerificacion ?? perfil.FechaVerificacion;
            perfil.QuienVerifico = dto.QuienVerifico ?? perfil.QuienVerifico;
            perfil.Destacado = dto.Destacado ?? perfil.Destacado;
            perfil.FechaInicioDestacado = dto.FechaInicioDestacado ?? perfil.FechaInicioDestacado;
            perfil.FechaFinDestacado = dto.FechaFinDestacado ?? perfil.FechaFinDestacado;
            perfil.Estado = dto.Estado ?? perfil.Estado;

            perfil.FechaActualizacion = DateTime.UtcNow;

            await _perfilRepository.SaveChangesAsync();
            return MapToDto(perfil);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var perfil = await _perfilRepository.Query().FirstOrDefaultAsync(p => p.IdPerfil == id);
            if (perfil == null) return false;

            _perfilRepository.Remove(perfil);  // Llamada al repositorio para eliminar el perfil
            await _perfilRepository.SaveChangesAsync();  // Guardamos los cambios
            return true;
        }


        private static PerfilDto MapToDto(Perfile p)
        {
            return new PerfilDto
            {
                Id = p.IdPerfil,
                IdUsuario = p.IdUsuario,
                IdAgencia = p.IdAgencia,
                NombrePerfil = p.NombrePerfil,
                Genero = p.Genero,
                Edad = p.Edad,
                Altura = p.Altura,
                Peso = p.Peso,
                Medidas = p.Medidas,
                ColorOjos = p.ColorOjos,
                ColorCabello = p.ColorCabello,
                Nacionalidad = p.Nacionalidad,
                Idiomas = p.Idiomas,
                Descripcion = p.Descripcion,
                Servicios = p.Servicios,
                Tarifas = p.Tarifas,
                UbicacionCiudad = p.UbicacionCiudad,
                UbicacionZona = p.UbicacionZona,
                Disponibilidad = p.Disponibilidad,
                DisponiblePara = p.DisponiblePara,
                Disponible24h = p.Disponible24h,
                DisponeLocal = p.DisponeLocal,
                HaceSalidas = p.HaceSalidas,
                Verificado = p.Verificado,
                FechaVerificacion = p.FechaVerificacion,
                QuienVerifico = p.QuienVerifico,
                EsIndependiente = p.EsIndependiente,
                TelefonoContacto = p.TelefonoContacto,
                Whatsapp = p.Whatsapp,
                EmailContacto = p.EmailContacto,
                FechaRegistro = p.FechaRegistro,
                FechaActualizacion = p.FechaActualizacion,
                Estado = p.Estado,
                Destacado = p.Destacado,
                FechaInicioDestacado = p.FechaInicioDestacado,
                FechaFinDestacado = p.FechaFinDestacado,
                UltimoOnline = p.UltimoOnline,
                NumVisitas = p.NumVisitas,
                NumContactos = p.NumContactos,
                PuntuacionInterna = p.PuntuacionInterna,
                NivelPopularidad = p.NivelPopularidad
            };
        }
    }
}
