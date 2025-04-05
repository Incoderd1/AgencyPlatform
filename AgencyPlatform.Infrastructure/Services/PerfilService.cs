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
            var perfiles = await _perfilRepository.Query().ToListAsync();
            return perfiles.Select(MapToDto).ToList();
        }

        public async Task<PerfilDto?> GetByIdAsync(int id)
        {
            var perfil = await _perfilRepository.Query().FirstOrDefaultAsync(p => p.id_perfil == id);
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

            var perfil = new perfile
            {
                id_usuario = dto.IdUsuario,
                id_agencia = dto.IdAgencia,
                nombre_perfil = dto.NombrePerfil,
                genero = dto.Genero,
                edad = (short)(dto.Edad ?? 18),
                altura = dto.Altura,
                peso = dto.Peso,
                medidas = dto.Medidas,
                color_ojos = dto.ColorOjos,
                color_cabello = dto.ColorCabello,
                nacionalidad = dto.Nacionalidad,
                descripcion = dto.Descripcion,
                ubicacion_ciudad = dto.UbicacionCiudad,
                ubicacion_zona = dto.UbicacionZona,
                disponible_para = dto.DisponiblePara ?? "todos",
                disponible_24h = dto.Disponible24h ?? false,
                dispone_local = dto.DisponeLocal ?? false,
                hace_salidas = dto.HaceSalidas ?? false,
                es_independiente = dto.EsIndependiente ?? false,
                telefono_contacto = dto.TelefonoContacto,
                whatsapp = dto.Whatsapp,
                email_contacto = dto.EmailContacto,
                fecha_registro = DateTime.Now,
                fecha_actualizacion = DateTime.Now,
                estado = "pendiente",
                destacado = false,
                nivel_popularidad = "bajo"
            };

            // Manejar los campos JSONB
            if (dto.Idiomas != null)
                perfil.idiomas = JsonSerializer.Serialize(dto.Idiomas);

            if (dto.Servicios != null)
                perfil.servicios = JsonSerializer.Serialize(dto.Servicios);

            if (dto.Tarifas != null)
                perfil.tarifas = JsonSerializer.Serialize(dto.Tarifas);

            if (dto.Disponibilidad != null)
                perfil.disponibilidad = JsonSerializer.Serialize(dto.Disponibilidad);

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

        public async Task<PerfilDto?> UpdateAsync(int id, UpdatePerfilDto dto)
        {
            var perfil = await _perfilRepository.Query().FirstOrDefaultAsync(p => p.id_perfil == id);
            if (perfil == null) return null;

            perfil.nombre_perfil = dto.NombrePerfil ?? perfil.nombre_perfil;
            perfil.genero = dto.Genero ?? perfil.genero;
            perfil.edad = (short)(dto.Edad ?? perfil.edad);
            perfil.altura = dto.Altura ?? perfil.altura;
            perfil.peso = dto.Peso ?? perfil.peso;
            perfil.medidas = dto.Medidas ?? perfil.medidas;
            perfil.color_ojos = dto.ColorOjos ?? perfil.color_ojos;
            perfil.color_cabello = dto.ColorCabello ?? perfil.color_cabello;
            perfil.nacionalidad = dto.Nacionalidad ?? perfil.nacionalidad;

            if (dto.Idiomas is not null)
                perfil.idiomas = JsonSerializer.Serialize(dto.Idiomas);

            if (dto.Descripcion is string descripcionStr)
                perfil.descripcion = descripcionStr;

            if (dto.Servicios is not null)
                perfil.servicios = JsonSerializer.Serialize(dto.Servicios);

            if (dto.Tarifas is not null)
                perfil.tarifas = JsonSerializer.Serialize(dto.Tarifas);

            perfil.ubicacion_ciudad = dto.UbicacionCiudad ?? perfil.ubicacion_ciudad;
            perfil.ubicacion_zona = dto.UbicacionZona ?? perfil.ubicacion_zona;

            if (dto.Disponibilidad is not null)
                perfil.disponibilidad = JsonSerializer.Serialize(dto.Disponibilidad);

            perfil.disponible_para = dto.DisponiblePara ?? perfil.disponible_para;
            perfil.disponible_24h = dto.Disponible24h ?? perfil.disponible_24h;
            perfil.dispone_local = dto.DisponeLocal ?? perfil.dispone_local;
            perfil.hace_salidas = dto.HaceSalidas ?? perfil.hace_salidas;
            perfil.telefono_contacto = dto.TelefonoContacto ?? perfil.telefono_contacto;
            perfil.whatsapp = dto.Whatsapp ?? perfil.whatsapp;
            perfil.email_contacto = dto.EmailContacto ?? perfil.email_contacto;

            perfil.verificado = dto.Verificado ?? perfil.verificado;
            perfil.fecha_verificacion = dto.FechaVerificacion ?? perfil.fecha_verificacion;
            perfil.quien_verifico = dto.QuienVerifico ?? perfil.quien_verifico;
            perfil.destacado = dto.Destacado ?? perfil.destacado;
            perfil.fecha_inicio_destacado = dto.FechaInicioDestacado ?? perfil.fecha_inicio_destacado;
            perfil.fecha_fin_destacado = dto.FechaFinDestacado ?? perfil.fecha_fin_destacado;
            perfil.estado = dto.Estado ?? perfil.estado;

            perfil.fecha_actualizacion = DateTime.UtcNow;

            await _perfilRepository.SaveChangesAsync();
            return MapToDto(perfil);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var perfil = await _perfilRepository.Query().FirstOrDefaultAsync(p => p.id_perfil == id);
            if (perfil == null) return false;

            _perfilRepository.Remove(perfil);  // Llamada al repositorio para eliminar el perfil
            await _perfilRepository.SaveChangesAsync();  // Guardamos los cambios
            return true;
        }


        private static PerfilDto MapToDto(perfile p)
        {
            return new PerfilDto
            {
                Id = p.id_perfil,
                IdUsuario = p.id_usuario,
                IdAgencia = p.id_agencia,
                NombrePerfil = p.nombre_perfil,
                Genero = p.genero,
                Edad = p.edad,
                Altura = p.altura,
                Peso = p.peso,
                Medidas = p.medidas,
                ColorOjos = p.color_ojos,
                ColorCabello = p.color_cabello,
                Nacionalidad = p.nacionalidad,
                Idiomas = p.idiomas,
                Descripcion = p.descripcion,
                Servicios = p.servicios,
                Tarifas = p.tarifas,
                UbicacionCiudad = p.ubicacion_ciudad,
                UbicacionZona = p.ubicacion_zona,
                Disponibilidad = p.disponibilidad,
                DisponiblePara = p.disponible_para,
                Disponible24h = p.disponible_24h,
                DisponeLocal = p.dispone_local,
                HaceSalidas = p.hace_salidas,
                Verificado = p.verificado,
                FechaVerificacion = p.fecha_verificacion,
                QuienVerifico = p.quien_verifico,
                EsIndependiente = p.es_independiente,
                TelefonoContacto = p.telefono_contacto,
                Whatsapp = p.whatsapp,
                EmailContacto = p.email_contacto,
                FechaRegistro = p.fecha_registro,
                FechaActualizacion = p.fecha_actualizacion,
                Estado = p.estado,
                Destacado = p.destacado,
                FechaInicioDestacado = p.fecha_inicio_destacado,
                FechaFinDestacado = p.fecha_fin_destacado,
                UltimoOnline = p.ultimo_online,
                NumVisitas = p.num_visitas,
                NumContactos = p.num_contactos,
                PuntuacionInterna = p.puntuacion_interna,
                NivelPopularidad = p.nivel_popularidad
            };
        }
    }
}
