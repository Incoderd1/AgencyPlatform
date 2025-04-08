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
        // Método para obtener todas las visitas de perfil
        Task<List<VisitaPerfilDto>> ObtenerTodasAsync();

        // Método para obtener una visita de perfil por ID
        Task<VisitaPerfilDto?> ObtenerPorIdAsync(int id);

        // Método para crear una nueva visita de perfil
        Task<VisitaPerfilDto> CrearAsync(CrearVisitaPerfilDto dto);

        // Método para eliminar una visita de perfil
        Task<bool> EliminarAsync(int id);

        // Método para contar las visitas de un perfil específico
        Task<int> ContarVisitasPorPerfil(int idPerfil);

    }
}
