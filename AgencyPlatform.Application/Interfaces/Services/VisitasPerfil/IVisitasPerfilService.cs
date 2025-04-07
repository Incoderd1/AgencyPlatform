using AgencyPlatform.Application.DTOs.VisitasPerfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services.VisitasPerfil
{
    public interface IVisitasPerfilService
    {
        Task<List<VisitaPerfilDto>> ObtenerTodasAsync();
        Task<VisitaPerfilDto?> ObtenerPorIdAsync(int id);
        Task<VisitaPerfilDto> CrearAsync(CrearVisitaPerfilDto dto);
        Task<bool> EliminarAsync(int id);
        Task<int> ContarVisitasPorPerfil(int idPerfil);

    }
}
