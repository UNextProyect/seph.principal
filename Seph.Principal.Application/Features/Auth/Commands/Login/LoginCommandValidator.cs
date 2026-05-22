using FluentValidation;

namespace Seph.Principal.Application.Features.Auth.Commands.Login
{
    /*Esta clase es un validador para el comando de inicio de sesión (LoginCommand) utilizando FluentValidation. 
     * Actualmente, no se han definido reglas de validación específicas, pero esta clase se puede expandir en el 
     * futuro para incluir validaciones como verificar que el correo electrónico tenga un formato válido o que la 
     * contraseña cumpla con ciertos requisitos de seguridad. */
    public sealed class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            /*Aquí es donde se pueden agregar las reglas de validación para el comando de inicio de sesión. 
            * Por ejemplo, se podrían agregar reglas para validar que el correo electrónico tenga un formato válido 
            * o que la contraseña cumpla con ciertos requisitos de seguridad. */
            RuleFor(request => request.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(request => request.Password ).NotEmpty().MinimumLength(8).MaximumLength(128);
            RuleFor(request => request.DeviceId).NotEmpty().MaximumLength(128);
            RuleFor(request => request.IpAddress).NotEmpty().MaximumLength(64);
        }
    }
}
