using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.Auth.Commands.Login;
using Seph.Principal.Application.Features.Auth.Commands.RefreshToken;
using Seph.Principal.Application.Features.Auth.Commands.RevokeSession;
using Seph.Principal.Application.Features.Auth.DTOs;

namespace Seph.Principal.Controllers
{
    public sealed class AuthController(ISender sender):ApiControllerBase
    {
        #region Login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new LoginCommand(
                request.Email,
                request.Password,
                Request.Headers["X-Device-Id"].FirstOrDefault() ?? "unknown",
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"), cancellationToken);

            return FromResponse(response);
        }

        #endregion

        #region Refresh Token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new RefreshTokenCommand(
                request.RefreshToken,
                Request.Headers["X-Device-Id"].FirstOrDefault() ?? "unknown",
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"), cancellationToken);

            return FromResponse(response);
        }
        #endregion

        #region logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
            => FromResponse(await sender.Send(new RevokeSessionCommand(request.RefreshToken), cancellationToken));
        #endregion

        #region Record
        public sealed record RefreshTokenRequest(string RefreshToken);
        #endregion

    }
}
