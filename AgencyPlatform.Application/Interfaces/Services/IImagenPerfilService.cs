using AgencyPlatform.Application.DTOs.PerfilImagen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services
{
    public interface IImagenPerfilService
    {
        Task<ImagenPerfilDto> CreateAsync(CrearImagenPerfilDto dto);
        Task<ImagenPerfilDto> UpdateAsync(int idImagen, UpdateImagenPerfilDto dto);
        Task<bool> DeleteAsync(int idImagen);
        Task<ImagenPerfilDto?> GetByIdAsync(int idImagen);
        Task<List<ImagenPerfilDto>> GetByPerfilAsync(int idPerfil);
    }
}
