using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPlatform.Application.DTOs.CuponesCliente;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace AgencyPlatform.Infrastructure.Services
{
    public class CuponesClienteService : ICuponesClienteService
    {
        private readonly ICuponesClienteRepository _repository;
        private readonly ILogger<CuponesClienteService> _logger;

        public CuponesClienteService(ICuponesClienteRepository repository, ILogger<CuponesClienteService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<CuponClienteDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return entities.Select(c => new CuponClienteDto
            {
                IdCuponCliente = c.IdCuponCliente,
                IdCliente = c.IdCliente,
                IdCupon = c.IdCupon,
                FechaAsignacion = c.FechaAsignacion,
                FechaUso = c.FechaUso,
                Estado = c.Estado,
                IdTransaccion = c.IdTransaccion,
                PuntosCanjeados = c.PuntosCanjeados
            }).ToList();
        }

        public async Task<CuponClienteDto?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            if (c == null) return null;

            return new CuponClienteDto
            {
                IdCuponCliente = c.IdCuponCliente,
                IdCliente = c.IdCliente,
                IdCupon = c.IdCupon,
                FechaAsignacion = c.FechaAsignacion,
                FechaUso = c.FechaUso,
                Estado = c.Estado,
                IdTransaccion = c.IdTransaccion,
                PuntosCanjeados = c.PuntosCanjeados
            };
        }

        public async Task<int> CreateAsync(CrearCuponClienteDto dto)
        {
            var entity = new CuponesCliente
            {
                IdCliente = dto.IdCliente,
                IdCupon = dto.IdCupon,
                FechaAsignacion = DateTime.UtcNow,
                Estado = "disponible", // default según tu base de datos
                IdTransaccion = dto.IdTransaccion,
                PuntosCanjeados = dto.PuntosCanjeados
            };

            try
            {
                await _repository.AddAsync(entity);
                await _repository.SaveAsync();
                return entity.IdCuponCliente;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el cupon cliente");
                throw new Exception("Error al guardar el cupon cliente: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<bool> UpdateAsync(int id, UpdateCuponClienteDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.FechaUso = dto.FechaUso ?? entity.FechaUso;
            entity.Estado = dto.Estado ?? entity.Estado;
            entity.IdTransaccion = dto.IdTransaccion ?? entity.IdTransaccion;
            entity.PuntosCanjeados = dto.PuntosCanjeados ?? entity.PuntosCanjeados;

            try
            {
                await _repository.UpdateAsync(entity);
                await _repository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cupon cliente");
                throw new Exception("Error al actualizar el cupon cliente: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            try
            {
                await _repository.DeleteAsync(entity);
                await _repository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cupon cliente");
                throw new Exception("Error al eliminar el cupon cliente: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
