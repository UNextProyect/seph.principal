using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Auth.Commands.GoogleLogin
{
    public sealed class GoogleLoginCommandHandler(
        IIdentityService identityService,
        IJwtTokenService jwtTokenService,
        IRefreshTokenSessionRepository sessionRepository,
        IUnitOfWork unitOfWork,
        IGoogleTokenValidator googleTokenValidator
    ) : IRequestHandler<GoogleLoginCommand, ResponseWrapper<AuthResponseDto>>
    {
        public async Task<ResponseWrapper<AuthResponseDto>> Handle(
            GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            //Validar el ID Token contra Google
            var payload = await googleTokenValidator.ValidateAsync(request.IdToken, cancellationToken);
            if (payload is null)
                return ResponseFactory.Failure<AuthResponseDto>("Token de Google inválido", HttpStatusCode.Unauthorized);

            //Buscar o crear usuario en Identity
            var user = await identityService.FindOrCreateGoogleUserAsync(
                payload.Email, payload.Name, payload.Subject, cancellationToken);

            if (user is null)
                return ResponseFactory.Failure<AuthResponseDto>("No se pudo autenticar con Google", HttpStatusCode.Unauthorized);

            var refreshToken = jwtTokenService.CreateRefreshToken();
            var refreshTokenHash = jwtTokenService.HashRefreshToken(refreshToken);
            var expiresAtUtc = DateTimeOffset.UtcNow.AddDays(7);
            var session = RefreshTokenSession.Create(user.Id, refreshTokenHash, request.DeviceId, request.IpAddress, expiresAtUtc);

            await sessionRepository.AddAsync(session, cancellationToken);
            await identityService.MarkLastLoginAsync(user.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var accessToken = jwtTokenService.CreateAccessToken(user);

            var response = new AuthResponseDto(
                accessToken,
                refreshToken,
                DateTimeOffset.UtcNow.AddMinutes(15),
                new UserSessionDto(
                    user.Id,
                    user.Email,
                    user.FullName,
                    user.IdInstitucion,
                    user.Roles,
                    user.Permissions
                )
            );

            return ResponseFactory.Success(response, "Autenticación con Google exitosa");
        }
    }

}