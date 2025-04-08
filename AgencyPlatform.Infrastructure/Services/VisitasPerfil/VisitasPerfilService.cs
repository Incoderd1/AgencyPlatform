using DeviceDetectorNET;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Application.DTOs.VisitasPerfil;
using AgencyPlatform.Application.Interfaces.Services.VisitasPerfil;
using AgencyPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPlatform.Infrastructure.Services.VisitasPerfil
{
    public class VisitasPerfilService : IVisitasPerfilService
    {
        private readonly IVisitasPerfilRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VisitasPerfilService(IVisitasPerfilRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
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
            var httpContext = _httpContextAccessor.HttpContext;

            // Obtener la IP automáticamente
            string ip = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Desconocida";

            // Obtener el User-Agent automáticamente
            string userAgent = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "Desconocido";

            // Analizar el User-Agent para detectar el dispositivo
            string dispositivo = DetectDevice(userAgent);

            // Calcular el tiempo de visita, en este caso podemos poner un valor fijo
            int tiempoVisita = 60; // O puedes agregar una lógica para calcularlo

            // Obtener el origen (en este caso lo dejamos como "web")
            string origen = "web";

            // Crear la entidad con los valores calculados y automáticos
            var entidad = new AgencyPlatform.Infrastructure.Data.Entities.VisitasPerfil
            {
                IdPerfil = dto.IdPerfil,
                IdCliente = dto.IdCliente,
                FechaVisita = DateTime.UtcNow,
                IpVisitante = ip,
                UserAgent = userAgent,
                TiempoVisita = tiempoVisita,
                Dispositivo = dispositivo,
                Origen = origen,
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

        // Método para detectar el dispositivo a partir del User-Agent
        private string DetectDevice(string userAgent)
        {
            var deviceDetector = new DeviceDetector(userAgent);
            deviceDetector.Parse();

            if (deviceDetector.IsBot())
            {
                return "Bot"; // Si es un bot
            }

            if (deviceDetector.IsMobile())
            {
                return "Mobile"; // Si es un dispositivo móvil
            }

            if (deviceDetector.IsTablet())
            {
                return "Tablet"; // Si es una tablet
            }

            return "Desktop"; // Si no es ninguno de los anteriores, es un desktop
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
