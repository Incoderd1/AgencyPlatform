using AgencyPlatform.Application.DTOs.Clientes;
using AgencyPlatform.Application.Interfaces.Repositories;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using AgencyPlatform.Shared.Helpers;

namespace AgencyPlatform.Infrastructure.Services
{
    public class ClienteService : IClienteService
    {
     
            private readonly IClienteRepository _clienteRepository;

            public ClienteService(IClienteRepository clienteRepository)
            {
                _clienteRepository = clienteRepository;
            }

            public async Task<ClienteDto> CreateAsync(int idUsuario, CrearClienteDto dto)
            {
                var nuevo = new Cliente
                {
                    IdUsuario = idUsuario,
                    Nombre = dto.Nombre,
                    Telefono = dto.Telefono,
                    Genero = dto.Genero,
                    FechaNacimiento = dto.FechaNacimiento,
                    Preferencias = dto.Preferencias != null ? Newtonsoft.Json.JsonConvert.SerializeObject(dto.Preferencias) : null,
                    Intereses = dto.Intereses != null ? Newtonsoft.Json.JsonConvert.SerializeObject(dto.Intereses) : null,
                    OrigenRegistro = dto.OrigenRegistro,
                    UbicacionHabitual = dto.UbicacionHabitual,
                    FechaRegistro = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow
                };

                await _clienteRepository.AddAsync(nuevo);
                await _clienteRepository.SaveChangesAsync();

                return MapToDto(nuevo);
            }

            public async Task<ClienteDto> UpdateAsync(int idCliente, UpdateClienteDto dto)
            {
                var cliente = await _clienteRepository.GetByIdAsync(idCliente)
                              ?? throw new Exception("Cliente no encontrado");

                cliente.Nombre = dto.Nombre ?? cliente.Nombre;
                cliente.Telefono = dto.Telefono ?? cliente.Telefono;
                cliente.Genero = dto.Genero ?? cliente.Genero;
                cliente.FechaNacimiento = dto.FechaNacimiento ?? cliente.FechaNacimiento;
                cliente.UbicacionHabitual = dto.UbicacionHabitual ?? cliente.UbicacionHabitual;
                cliente.Preferencias = dto.Preferencias != null ? Newtonsoft.Json.JsonConvert.SerializeObject(dto.Preferencias) : cliente.Preferencias;
                cliente.Intereses = dto.Intereses != null ? Newtonsoft.Json.JsonConvert.SerializeObject(dto.Intereses) : cliente.Intereses;
                cliente.FechaActualizacion = DateTime.UtcNow;

                await _clienteRepository.SaveChangesAsync();

                return MapToDto(cliente);
            }

            public async Task<ClienteDto> GetByIdAsync(int idCliente)
            {
                var cliente = await _clienteRepository.GetByIdAsync(idCliente)
                              ?? throw new Exception("Cliente no encontrado");

                return MapToDto(cliente);
            }

            public async Task<ClienteResumenDto> GetResumenByIdAsync(int idCliente)
            {
                var resumen = await _clienteRepository.GetResumenByIdAsync(idCliente)
                              ?? throw new Exception("Cliente no encontrado");

                return resumen;
            }

            public async Task<List<ClienteResumenDto>> GetAllAsync()
            {
                return await _clienteRepository.GetAllResumenAsync();
            }

            public async Task<bool> DeleteAsync(int idCliente)
            {
                var cliente = await _clienteRepository.GetByIdAsync(idCliente);
                if (cliente == null) return false;

                _clienteRepository.Delete(cliente);
                await _clienteRepository.SaveChangesAsync();
                return true;
            }
        public async Task<PaginatedResult<ClienteResumenDto>> GetPaginatedAsync(int page, int pageSize)
        {
            var query = _clienteRepository.QueryResumen(); // IQueryable<ClienteResumenDto>
            return await PaginationHelper.CreateAsync(query, page, pageSize);
        }



        private static ClienteDto MapToDto(Cliente c) => new ClienteDto
            {
                IdCliente = c.IdCliente,
                IdUsuario = c.IdUsuario,
                Nombre = c.Nombre,
                Telefono = c.Telefono,
                Genero = c.Genero,
                Edad = c.Edad,
                EsVip = c.EsVip,
                NivelVip = c.NivelVip,
                FechaNacimiento = c.FechaNacimiento,
                Preferencias = string.IsNullOrWhiteSpace(c.Preferencias) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<object>(c.Preferencias),
                Intereses = string.IsNullOrWhiteSpace(c.Intereses) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<object>(c.Intereses),
                UltimaActividad = c.UltimaActividad,
                FidelidadScore = c.FidelidadScore
            };
        }
    }

