using FluentValidation;

namespace Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion
{
    public sealed class CreateInstitucionCommandValidator : AbstractValidator<CreateInstitucionCommand>
    {
        public CreateInstitucionCommandValidator()
        {
            RuleFor(x => x.StrNombre)
                .NotEmpty().WithMessage("El nombre de la institución es obligatorio.")
                .MaximumLength(250);

            RuleFor(x => x.IdMunicipio)
                .GreaterThan(0).WithMessage("Debe indicar el municipio de la institución.");

            RuleFor(x => x.StrCorreoInstitucional)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.StrCorreoInstitucional))
                .WithMessage("El correo institucional no es válido.");
        }
    }
}
