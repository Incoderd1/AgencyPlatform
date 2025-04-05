using AgencyPlatform.Application.DTOs.Auth;
using FluentValidation;

namespace AgencyPlatform.Application.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(x => x.TipoUsuario)
                .NotEmpty().WithMessage("El tipo de usuario es obligatorio.")
                .Must(t => new[] { "cliente", "admin", "agencia", "moderador", "soporte" }.Contains(t))
                .WithMessage("Tipo de usuario no válido.");
        }
    }
}
