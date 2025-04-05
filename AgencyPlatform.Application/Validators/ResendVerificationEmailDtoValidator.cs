using AgencyPlatform.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Validators
{
    public class ResendVerificationEmailDtoValidator : AbstractValidator<ResendVerificationEmailDto>
    {
        public ResendVerificationEmailDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.");
        }
    }
}
