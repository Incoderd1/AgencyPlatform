using AgencyPlatform.Application.DTOs.Agencia;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Validators
{
    public class CrearAgenciaDtoValidator : AbstractValidator<CrearAgenciaDto>
    {
        public CrearAgenciaDtoValidator()
        {
            RuleFor(x => x.NombreComercial)
                .NotEmpty().WithMessage("Nombre comercial es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(20);

            RuleFor(x => x.EmailContacto)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("Formato de correo inválido.");

            // Añadir reglas adicionales según necesidad.
        }
    }
}
