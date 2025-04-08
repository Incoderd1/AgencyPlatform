using AgencyPlatform.Application.DTOs.VisitasPerfil;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Validators.VisitasPerfil
{
    public class CrearVisitaPerfilDtoValidator : AbstractValidator<CrearVisitaPerfilDto>
    {
        public CrearVisitaPerfilDtoValidator()
        {
            RuleFor(x => x.IdPerfil)
                .NotEmpty().WithMessage("El IdPerfil no puede ser vacío.")
                .GreaterThan(0).WithMessage("El IdPerfil debe ser mayor que 0.");

            RuleFor(x => x.IdCliente)
                .NotEmpty().WithMessage("El IdCliente no puede ser vacío.");

            RuleFor(x => x.FechaVisita)
                .NotEmpty().WithMessage("La FechaVisita no puede ser vacía.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La FechaVisita no puede ser en el futuro.");

        }
    }
}

