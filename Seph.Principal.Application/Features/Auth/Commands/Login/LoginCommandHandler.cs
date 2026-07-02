using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.Login
{
    public sealed class LoginCommandHandler(IIdentityService identityService, IJwtTokenService jwtTokenService, IRefreshTokenSessionRepository sessionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<LoginCommand, ResponseWrapper<AuthResponseDto>>
    {

        public async Task<ResponseWrapper<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Validamos las credenciales del usuario utilizando el IIdentityService,
            // si las credenciales son inválidas, se devuelve una respuesta de error con un mensaje y un código de estado HTTP 401.
            var user = await identityService.ValidateCredentialAsync(request.Email, request.Password, cancellationToken);
            if (user is null)
            {
                return ResponseFactory.Failure<AuthResponseDto>("Crendeicales invalidas", HttpStatusCode.Unauthorized);

            }

            /*1.1. Verificamos que el correo del usuario esté confirmado antes de continuar. */
            if (!user.EmailConfirmed)
            {
                return ResponseFactory.Failure<AuthResponseDto>("Debes verificar tu correo electrónico antes de iniciar sesión", HttpStatusCode.Forbidden);
            }

            /* refrescamos el token de acceso y el token de refresco utilizando el IJwtTokenService. */
            var refreshToken = jwtTokenService.CreateRefreshToken();
            /*creamos el hash del token de refresco */
            var refreshTokenHash = jwtTokenService.HashRefreshToken(refreshToken);
            /*generamos la fecha de expiración del token de refresco  7 dias*/
            var expiresAtUtc = DateTimeOffset.UtcNow.AddDays(7);
            /*generamos la sesión del token de refresco */
            var session = RefreshTokenSession.Create(user.Id, refreshTokenHash, request.DeviceId, request.IpAddress, expiresAtUtc);
            /*agregamos la sesión del token de refresco al repositorio */
            await sessionRepository.AddAsync(session, cancellationToken);
            /*marcamos la última vez que el usuario inició sesión */
            await identityService.MarkLastLoginAsync(user.Id, cancellationToken);
            /*generamos el token de acceso */
            await unitOfWork.SaveChangesAsync(cancellationToken);

            /*generamos el token de acceso */
            var accessToken = jwtTokenService.CreateAccessToken(user);
            /*creamos la respuesta  con los tokens y la información del usuario */
            var response = new AuthResponseDto(
                accessToken, refreshToken, DateTimeOffset.UtcNow.AddMinutes(15),
            new UserSessionDto(user.Id, user.Email, user.FullName, user.IdInstitucion, user.Roles, user.Permissions));
            /*regresamos la respuesta  que es una entidad del tipo responseWrapper*/
            return ResponseFactory.Success(response, "Autenticación exitosa");
        }
    }


}