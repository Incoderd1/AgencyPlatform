using FluentValidation;
using AgencyPlatform.Application.DTOs.CuponesCliente;

namespace AgencyPlatform.Application.Validators.CuponesCliente
{
    public class CrearCuponClienteValidator : AbstractValidator<CrearCuponClienteDto>
    {
        public CrearCuponClienteValidator()
        {
            RuleFor(x => x.IdCupon)
                .GreaterThan(0).WithMessage("El ID del cupón debe ser mayor que cero.");

            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor que cero.");

            RuleFor(x => x.PuntosCanjeados)
                .GreaterThanOrEqualTo(0).When(x => x.PuntosCanjeados.HasValue)
                .WithMessage("Los puntos canjeados no pueden ser negativos.");
        }
    }
}
