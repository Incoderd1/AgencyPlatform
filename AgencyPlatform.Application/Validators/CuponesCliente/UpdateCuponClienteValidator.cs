using FluentValidation;
using AgencyPlatform.Application.DTOs.CuponesCliente;

namespace AgencyPlatform.Application.Validators.CuponesCliente
{
    public class UpdateCuponClienteValidator : AbstractValidator<UpdateCuponClienteDto>
    {
        public UpdateCuponClienteValidator()
        {
            RuleFor(x => x.Estado)
                .Must(BeAValidEstado)
                .When(x => x.Estado != null)
                .WithMessage("El estado debe ser uno de: disponible, usado, expirado, cancelado.");

            RuleFor(x => x.PuntosCanjeados)
                .GreaterThanOrEqualTo(0).When(x => x.PuntosCanjeados.HasValue)
                .WithMessage("Los puntos canjeados no pueden ser negativos.");
        }

        private bool BeAValidEstado(string estado)
        {
            return new[] { "disponible", "usado", "expirado", "cancelado" }
                .Contains(estado.ToLower());
        }
    }
}
