using AgencyPlatform.Application.DTOs.Perfil;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Validators
{
    public class CrearPerfilDtoValidator : AbstractValidator<CrearPerfilDto>
    {
        public CrearPerfilDtoValidator()
        {
            // Validar que IdUsuario no sea cero o negativo
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El ID del usuario es obligatorio y debe ser mayor que 0.");

            // Validar que NombrePerfil no sea nulo o vacío
            RuleFor(x => x.NombrePerfil)
                .NotEmpty().WithMessage("El nombre del perfil es obligatorio.")
                .Length(1, 50).WithMessage("El nombre del perfil debe tener entre 1 y 50 caracteres.");

            // Validar que el género sea uno de los valores válidos
            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("El género es obligatorio.")
                .Must(g => new[] { "M", "F", "trans", "otro" }.Contains(g)).WithMessage("El género debe ser uno de los siguientes: 'M', 'F', 'trans', 'otro'.");

           

            // Validar que Idiomas, Servicios, Tarifas y Disponibilidad no estén vacíos si existen
            RuleFor(x => x.Idiomas)
                .Must(x => x == null || x.Any()).WithMessage("La lista de idiomas no puede estar vacía si se proporciona.");

            RuleFor(x => x.Servicios)
                .Must(x => x == null || x.Any()).WithMessage("La lista de servicios no puede estar vacía si se proporciona.");

            RuleFor(x => x.Tarifas)
                .Must(x => x == null || x.Any()).WithMessage("La lista de tarifas no puede estar vacía si se proporciona.");

            RuleFor(x => x.Disponibilidad)
                .Must(x => x == null || x.Any()).WithMessage("La lista de disponibilidad no puede estar vacía si se proporciona.");
        }
    }
}
