using FluentValidation;

namespace Seph.Principal.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(256);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(12).WithMessage("La contraseña debe tener al menos 12 caracteres.")
                .Matches("[A-Z]").WithMessage("Debe contener al menos una mayúscula.")
                .Matches("[a-z]").WithMessage("Debe contener al menos una minúscula.")
                .Matches("[0-9]").WithMessage("Debe contener al menos un número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Debe contener al menos un carácter especial.");

            RuleFor(x => x.IdInstitucion)
                .GreaterThan(0).WithMessage("El administrador no tiene una institución válida asignada.");
        }
    }
}
