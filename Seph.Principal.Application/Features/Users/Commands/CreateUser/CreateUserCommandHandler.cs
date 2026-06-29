using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler(IIdentityService identityService)
        : IRequestHandler<CreateUserCommand, ResponseWrapper<UserCreatedDto>>
    {
        private const string UserRole = "User";

        public async Task<ResponseWrapper<UserCreatedDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.CreateUserWithRoleAsync(
                request.FullName, request.Email, request.Password, UserRole, request.IdInstitucion, cancellationToken);

            if (userId is null)
                return ResponseFactory.Failure<UserCreatedDto>("El correo ya está registrado", HttpStatusCode.Conflict);

            var dto = new UserCreatedDto(userId.Value, request.Email, request.FullName, UserRole, request.IdInstitucion);
            return ResponseFactory.Success(dto, "Usuario creado correctamente");
        }
    }
}
