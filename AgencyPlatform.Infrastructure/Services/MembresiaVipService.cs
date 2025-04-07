using AgencyPlatform.Application.DTOs.MembresiasVip;
using AgencyPlatform.Application.Interfaces.Services.MembresiasVip;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Services
{
    public class MembresiaVipService : IMembresiaVipService
    {
       
            private readonly IMembresiaVipRepository _repository;

            public MembresiaVipService(IMembresiaVipRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<MembresiaVipDto>> ObtenerTodasAsync()
            {
                var lista = await _repository.GetAllAsync();
                return lista.Select(MapToDto).ToList();
            }

            public async Task<MembresiaVipDto?> ObtenerPorIdAsync(int id)
            {
                var entidad = await _repository.GetByIdAsync(id);
                return entidad is null ? null : MapToDto(entidad);
            }

            public async Task<MembresiaVipDto> CrearAsync(CrearMembresiaVipDto dto)
            {
                var entidad = new MembresiasVip
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    PrecioMensual = dto.PrecioMensual,
                    PrecioTrimestral = dto.PrecioTrimestral,
                    PrecioAnual = dto.PrecioAnual,
                    Beneficios = Newtonsoft.Json.JsonConvert.SerializeObject(dto.Beneficios),
                    PuntosMensuales = dto.PuntosMensuales,
                    ReduccionAnuncios = dto.ReduccionAnuncios,
                    DescuentosAdicionales = dto.DescuentosAdicionales,
                    Estado = "activo",
                    FechaCreacion = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow
                };

                await _repository.AddAsync(entidad);
                await _repository.SaveChangesAsync();
                return MapToDto(entidad);
            }

            public async Task<MembresiaVipDto?> ActualizarAsync(int id, UpdateMembresiaVipDto dto)
            {
                var entidad = await _repository.GetByIdAsync(id);
                if (entidad == null) return null;

                entidad.Nombre = dto.Nombre ?? entidad.Nombre;
                entidad.Descripcion = dto.Descripcion ?? entidad.Descripcion;
                entidad.PrecioMensual = dto.PrecioMensual ?? entidad.PrecioMensual;
                entidad.PrecioTrimestral = dto.PrecioTrimestral ?? entidad.PrecioTrimestral;
                entidad.PrecioAnual = dto.PrecioAnual ?? entidad.PrecioAnual;
                entidad.Beneficios = dto.Beneficios != null ? Newtonsoft.Json.JsonConvert.SerializeObject(dto.Beneficios) : entidad.Beneficios;
                entidad.PuntosMensuales = dto.PuntosMensuales ?? entidad.PuntosMensuales;
                entidad.ReduccionAnuncios = dto.ReduccionAnuncios ?? entidad.ReduccionAnuncios;
                entidad.DescuentosAdicionales = dto.DescuentosAdicionales ?? entidad.DescuentosAdicionales;
                entidad.Estado = dto.Estado ?? entidad.Estado;
                entidad.FechaActualizacion = DateTime.UtcNow;

                _repository.Update(entidad);
                await _repository.SaveChangesAsync();

                return MapToDto(entidad);
            }

            public async Task<bool> EliminarAsync(int id)
            {
                var entidad = await _repository.GetByIdAsync(id);
                if (entidad == null) return false;

                _repository.Delete(entidad);
                await _repository.SaveChangesAsync();
                return true;
            }

        private static MembresiaVipDto MapToDto(MembresiasVip m) => new()
        {
                    IdPlan = m.IdPlan,
                    Nombre = m.Nombre,
                    Descripcion = m.Descripcion,
                    PrecioMensual = m.PrecioMensual,
                    PrecioTrimestral = m.PrecioTrimestral,
                    PrecioAnual = m.PrecioAnual,
                    Beneficios = string.IsNullOrWhiteSpace(m.Beneficios)
             ? null
             : Newtonsoft.Json.JsonConvert.DeserializeObject<BeneficiosVipModel>(m.Beneficios),
                    PuntosMensuales = m.PuntosMensuales,
                    ReduccionAnuncios = m.ReduccionAnuncios,
                    DescuentosAdicionales = m.DescuentosAdicionales,
                    Estado = m.Estado,
                    FechaCreacion = m.FechaCreacion,
                    FechaActualizacion = m.FechaActualizacion
        };

    }

}

