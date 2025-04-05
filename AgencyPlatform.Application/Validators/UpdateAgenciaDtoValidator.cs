using AgencyPlatform.Application.DTOs.Agencia;
using FluentValidation;

namespace AgencyPlatform.Application.Validators
{
    public class UpdateAgenciaDtoValidator : AbstractValidator<UpdateAgenciaDto>
    {
        public UpdateAgenciaDtoValidator()
        {
            RuleFor(x => x.NombreComercial)
                .NotEmpty().WithMessage("El nombre comercial es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre comercial no puede superar los 100 caracteres.");

            RuleFor(x => x.RazonSocial)
                .MaximumLength(150).WithMessage("La razón social no puede superar los 150 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.RazonSocial));

            RuleFor(x => x.NifCif)
                .MaximumLength(20).WithMessage("El NIF/CIF no puede superar los 20 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.NifCif));

            RuleFor(x => x.Direccion)
                .MaximumLength(200).WithMessage("La dirección no puede superar los 200 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Direccion));

            RuleFor(x => x.Ciudad)
                .MaximumLength(100).WithMessage("La ciudad no puede superar los 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Ciudad));

            RuleFor(x => x.Region)
                .MaximumLength(100).WithMessage("La región no puede superar los 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Region));

            RuleFor(x => x.Pais)
                .MaximumLength(100).WithMessage("El país no puede superar los 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Pais));

            RuleFor(x => x.CodigoPostal)
                .MaximumLength(20).WithMessage("El código postal no puede superar los 20 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.CodigoPostal));

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(20).WithMessage("El teléfono no puede superar los 20 caracteres.");

            RuleFor(x => x.EmailContacto)
                .NotEmpty().WithMessage("El email de contacto es obligatorio.")
                .EmailAddress().WithMessage("El email de contacto debe ser una dirección válida.")
                .MaximumLength(100).WithMessage("El email no puede superar los 100 caracteres.");

            RuleFor(x => x.SitioWeb)
                .MaximumLength(200).WithMessage("El sitio web no puede superar los 200 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.SitioWeb));

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Descripcion));

            
        }
    }
}
