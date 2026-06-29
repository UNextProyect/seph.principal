using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;
using System.Net;

namespace Seph.Principal.Application.Features.Users.Commands.CreateAdmin
{
    public sealed class CreateAdminCommandHandler(IIdentityService identityService)
        : IRequestHandler<CreateAdminCommand, ResponseWrapper<UserCreatedDto>>
    {
        private const string AdminRole = "Admin";

        public async Task<ResponseWrapper<UserCreatedDto>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.CreateUserWithRoleAsync(
                request.FullName, request.Email, request.Password, AdminRole, request.IdInstitucion, cancellationToken);

            if (userId is null)
                return ResponseFactory.Failure<UserCreatedDto>("El correo ya está registrado", HttpStatusCode.Conflict);

            var dto = new UserCreatedDto(userId.Value, request.Email, request.FullName, AdminRole, request.IdInstitucion);
            return ResponseFactory.Success(dto, "Administrador de institución creado correctamente");
        }
    }
}
