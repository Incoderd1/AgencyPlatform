using AgencyPlatform.Application.DTOs.Puntos;

namespace AgencyPlatform.Application.Interfaces.Services.Puntos
{
    public interface IPuntoService
    {
        Task CrearAsync(CrearPuntoDto dto);
        Task<List<PuntoDto>> ObtenerPorClienteAsync(int idCliente);
        Task<ResumenPuntosDto> ObtenerResumenAsync(int idCliente);
    }
}
