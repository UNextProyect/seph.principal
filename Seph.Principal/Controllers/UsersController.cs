using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Application.Features.Users.Commands.CreateAdmin;
using Seph.Principal.Application.Features.Users.Commands.CreateUser;
using Seph.Principal.Application.Features.Users.DTOs;
using Seph.Principal.Application.Features.Users.Queries.GetCurrentUser;
using System.Net;

namespace Seph.Principal.Controllers
{
    [Authorize]
    public sealed class UsersController(ISender sender, ICurrentUserService currentUserService) : ApiControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken cancellationToken)
        {
            if (currentUserService.UserId is not { } userId)
            {
                return FromResponse(ResponseFactory.Failure<UserSessionDto>("Usuario no autenticado", HttpStatusCode.Unauthorized));
            }

            return FromResponse(await sender.Send(new GetCurrentUserQuery(userId), cancellationToken));
        }

        /// <summary>
        /// Metodo para crear un usuario administrador (SuperAdmin) ligado a una institución.
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("admins")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminRequest request, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(
                new CreateAdminCommand(request.FullName, request.Email, request.Password, request.IdInstitucion),
                cancellationToken));

        /// <summary>
        /// Metodo para crear un usuario ligado a la institución del administrador que realiza la petición.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (currentUserService.IdInstitucion is not { } idInstitucion)
            {
                return FromResponse(ResponseFactory.Failure<UserCreatedDto>(
                    "El administrador no tiene una institución asignada", HttpStatusCode.BadRequest));
            }

            return FromResponse(await sender.Send(
                new CreateUserCommand(request.FullName, request.Email, request.Password, idInstitucion),
                cancellationToken));
        }
    }

}
