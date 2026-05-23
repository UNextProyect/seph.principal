using FluentValidation;

namespace Seph.Principal.Application.Features.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(request => request.RefreshToken).NotEmpty().MinimumLength(32);
            RuleFor(request => request.DeviceId).NotEmpty().MaximumLength(128);
            RuleFor(request => request.IpAddress).NotEmpty().MaximumLength(64);
        }

    }
}
