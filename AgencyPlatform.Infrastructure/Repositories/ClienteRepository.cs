using AgencyPlatform.Application.DTOs.Clientes;
using AgencyPlatform.Application.Interfaces.Repositories;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AgencyPlatformDbContext _context;

        public ClienteRepository(AgencyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente entity)
        {
            await _context.Clientes.AddAsync(entity);
        }

        public void Delete(Cliente entity)
        {
            _context.Clientes.Remove(entity);
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        public async Task<ClienteResumenDto?> GetResumenByIdAsync(int id)
        {
            return await _context.Clientes
                .Where(c => c.IdCliente == id)
                .Select(c => new ClienteResumenDto
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    EsVip = c.EsVip,
                    NivelVip = c.NivelVip,
                    PuntosAcumulados = c.PuntosAcumulados,
                    Fidelidad = c.FidelidadScore,
                    UltimaActividad = c.UltimaActividad
                })
                .FirstOrDefaultAsync();
        }
        public IQueryable<ClienteResumenDto> QueryResumen()
        {
            return _context.Clientes
                .Include(c => c.IdUsuarioNavigation)
                .Select(c => new ClienteResumenDto
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    Edad = c.Edad,
                    EsVip = c.EsVip,
                    NivelVip = c.NivelVip,
                    UltimaActividad = c.UltimaActividad
                });
        }


        public async Task<List<ClienteResumenDto>> GetAllResumenAsync()
        {
            return await _context.Clientes
                .OrderByDescending(c => c.FechaRegistro)
                .Select(c => new ClienteResumenDto
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    EsVip = c.EsVip,
                    NivelVip = c.NivelVip,
                    PuntosAcumulados = c.PuntosAcumulados,
                    Fidelidad = c.FidelidadScore,
                    UltimaActividad = c.UltimaActividad
                })
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
