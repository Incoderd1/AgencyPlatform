using AgencyPlatform.Application.DTOs.PerfilImagen;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Validators.ImagenPerfil
{
    public class CrearImagenPerfilDtoValidator : AbstractValidator<CrearImagenPerfilDto>
    {
        public CrearImagenPerfilDtoValidator()
        {
            //RuleFor(x => x.IdPerfil).GreaterThan(0);
            RuleFor(x => x.UrlImagen).NotEmpty().MaximumLength(255);
            RuleFor(x => x.UrlThumbnail).NotEmpty().MaximumLength(255);
            RuleFor(x => x.TipoMime).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Ancho).GreaterThan(0);
            RuleFor(x => x.Alto).GreaterThan(0);
            RuleFor(x => x.TamañoBytes).GreaterThan(0);
            RuleFor(x => x.NivelBlurring).InclusiveBetween((short)0, (short)10);
        }
    }

    public class UpdateImagenPerfilDtoValidator : AbstractValidator<UpdateImagenPerfilDto>
    {
        public UpdateImagenPerfilDtoValidator()
        {
            RuleFor(x => x.NivelBlurring).InclusiveBetween((short)0, (short)10).When(x => x.NivelBlurring != null);
        }
    }
}
