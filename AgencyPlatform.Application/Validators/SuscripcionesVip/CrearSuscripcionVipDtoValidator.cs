using FluentValidation;
using AgencyPlatform.Application.DTOs.SuscripcionesVip;

namespace AgencyPlatform.Application.Validators.SuscripcionesVip
{
    public class CrearSuscripcionVipDtoValidator : AbstractValidator<CrearSuscripcionVipDto>
    {
        public CrearSuscripcionVipDtoValidator()
        {
            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("El ID del cliente es obligatorio y debe ser válido.");

            RuleFor(x => x.IdPlan)
                .GreaterThan(0).WithMessage("El ID del plan es obligatorio y debe ser válido.");

            RuleFor(x => x.NumeroSuscripcion)
                .NotEmpty().WithMessage("El número de suscripción es obligatorio.");

            RuleFor(x => x.FechaInicio)
                .LessThan(x => x.FechaFin).WithMessage("La fecha de inicio debe ser anterior a la fecha de fin.");

            RuleFor(x => x.TipoCiclo)
                .NotEmpty().WithMessage("El tipo de ciclo es obligatorio.");

            RuleFor(x => x.MontoBase)
                .GreaterThan(0).WithMessage("El monto base debe ser mayor a 0.");

            RuleFor(x => x.Moneda)
                .NotEmpty().WithMessage("El código de moneda es obligatorio.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado es obligatorio.");

            RuleFor(x => x.MetodoPago)
                .NotEmpty().WithMessage("El método de pago es obligatorio.");

            RuleFor(x => x.GatewayPago)
                .NotEmpty().WithMessage("El proveedor de pago es obligatorio.");

            RuleFor(x => x.SolicitadaPor)
                .NotEmpty().WithMessage("El campo solicitada por es obligatorio.");
        }
    }
}
