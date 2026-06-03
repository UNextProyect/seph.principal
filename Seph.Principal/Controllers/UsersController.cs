using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
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
    }

}
