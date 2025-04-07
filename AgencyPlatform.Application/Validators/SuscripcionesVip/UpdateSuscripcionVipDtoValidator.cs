using FluentValidation;
using AgencyPlatform.Application.DTOs.SuscripcionesVip;

namespace AgencyPlatform.Application.Validators.SuscripcionesVip
{
    public class UpdateSuscripcionVipDtoValidator : AbstractValidator<UpdateSuscripcionVipDto>
    {
        public UpdateSuscripcionVipDtoValidator()
        {
            When(x => x.FechaRenovacion.HasValue && x.FechaProximoCargo.HasValue, () =>
            {
                RuleFor(x => x.FechaRenovacion)
                    .LessThan(x => x.FechaProximoCargo)
                    .WithMessage("La fecha de renovación debe ser anterior a la fecha del próximo cargo.");
            });

            RuleFor(x => x.Estado)
                .MaximumLength(50).WithMessage("El estado no debe exceder los 50 caracteres.");

            RuleFor(x => x.IntentosCobro)
                .GreaterThanOrEqualTo(0).WithMessage("Los intentos de cobro no pueden ser negativos.");
        }
    }
}
