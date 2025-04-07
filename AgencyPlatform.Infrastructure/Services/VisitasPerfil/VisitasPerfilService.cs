using AgencyPlatform.Application.DTOs.VisitasPerfil;
using AgencyPlatform.Application.Interfaces.Services.VisitasPerfil;
using AgencyPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;  // Asegúrate de agregar este espacio de nombres
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Services.VisitasPerfil
{
    public class VisitasPerfilService : IVisitasPerfilService
    {
        private readonly IVisitasPerfilRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;  // Agregamos IHttpContextAccessor

        // Inyectar IHttpContextAccessor en el constructor
        public VisitasPerfilService(IVisitasPerfilRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;  // Asignamos el valor del IHttpContextAccessor
        }

        public async Task<List<VisitaPerfilDto>> ObtenerTodasAsync()
        {
            var visitas = await _repository.GetAllAsync();
            return visitas.Select(MapToDto).ToList();
        }

        public async Task<VisitaPerfilDto?> ObtenerPorIdAsync(int id)
        {
            var visita = await _repository.GetByIdAsync(id);
            return visita is null ? null : MapToDto(visita);
        }

        public async Task<VisitaPerfilDto> CrearAsync(CrearVisitaPerfilDto dto)
        {
            // Usar IHttpContextAccessor para obtener IP y UserAgent
            var httpContext = _httpContextAccessor.HttpContext;

            string ip = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Desconocida";
            string userAgent = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "Desconocido";

            var entidad = new AgencyPlatform.Infrastructure.Data.Entities.VisitasPerfil
            {
                IdPerfil = dto.IdPerfil,
                IdCliente = dto.IdCliente,
                FechaVisita = DateTime.UtcNow,
                IpVisitante = ip,
                UserAgent = userAgent,
                TiempoVisita = dto.TiempoVisita,
                Dispositivo = dto.Dispositivo,
                Origen = dto.Origen,
                RegionGeografica = dto.RegionGeografica
            };

            await _repository.AddAsync(entidad);
            await _repository.SaveChangesAsync();

            return MapToDto(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var visita = await _repository.GetByIdAsync(id);
            if (visita == null) return false;

            _repository.Delete(visita);
            await _repository.SaveChangesAsync();
            return true;
        }

        // Método para contar visitas por perfil
        public async Task<int> ContarVisitasPorPerfil(int idPerfil)
        {
            return await _repository.ContarVisitasPorPerfil(idPerfil);
        }

        private static VisitaPerfilDto MapToDto(AgencyPlatform.Infrastructure.Data.Entities.VisitasPerfil v) => new()
        {
            IdVisita = v.IdVisita,
            IdPerfil = v.IdPerfil,
            IdCliente = v.IdCliente,
            FechaVisita = v.FechaVisita,
            IpVisitante = v.IpVisitante,
            UserAgent = v.UserAgent,
            TiempoVisita = v.TiempoVisita,
            Dispositivo = v.Dispositivo,
            Origen = v.Origen,
            RegionGeografica = v.RegionGeografica
        };
    }
}
