using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AgencyPlatform.Application.DTOs.PerfilImagen
{
    public class UploadFileDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
