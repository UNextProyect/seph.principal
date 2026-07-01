using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Domain.Repositories;
using System.Net;

namespace Seph.Principal.Application.Features.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandHandler(IIdentityService identityService,
    IJwtTokenService jwtTokenService,
    IRefreshTokenSessionRepository sessionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RefreshTokenCommand, ResponseWrapper<AuthResponseDto>>
    {
        public async Task<ResponseWrapper<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            /*1. Validar el token de refresco utilizando el IJwtTokenService
             * para asegurarse de que sea válido y no haya expirado. */
            var tokenHash = jwtTokenService.HashRefreshToken(request.RefreshToken);
            /*2. Recuperar la sesión de usuario asociada al token de refresco desde el IRefreshTokenSessionRepository.
             * Verificar que la sesión esté activa y que el token de refresco coincida con el hash almacenado. */
            var session = await sessionRepository.GetActiveByTokenHashAsync(tokenHash, cancellationToken);
            /*3. Si la sesión es válida, generar un nuevo token de acceso y un nuevo token de refresco utilizando el IJwtTokenService.
             * Actualizar la sesión de usuario con el nuevo hash del token de refresco y la nueva fecha de expiración. */
            if (session is null || !session.IsActive || session.DeviceId != request.DeviceId)
            {
                return ResponseFactory.Failure<AuthResponseDto>("Sesión inválida o expirada", HttpStatusCode.Unauthorized);
            }
            
            /*4. Guardar los cambios en la base de datos utilizando el IUnitOfWork. */
            var user = await identityService.GetUserByIdAsync(session.UserId, cancellationToken);
            if (user is null) 
            {
                session.Revoke();
                /*5. Regresar una respuesta indicando el resultado de la operación, incluyendo el nuevo token de acceso 
                 * y el nuevo token de refresco si la operación fue exitosa, o un mensaje de error si no lo fue. */
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseFactory.Failure<AuthResponseDto>("Usuario no encontrado", HttpStatusCode.Unauthorized)!;
            }
            
            /*6. Implementar medidas de seguridad adicionales, como la revocación de tokens de refresco comprometidos 
             * o la limitación del número de tokens de refresco activos por usuario o dispositivo. */
            var newRefreshToken = jwtTokenService.CreateRefreshToken();
            session.Rotate(jwtTokenService.HashRefreshToken(newRefreshToken), DateTimeOffset.UtcNow.AddDays(7), request.IpAddress);
            /*7. Asegurarse de que el proceso de refresco de tokens sea eficiente y escalable, especialmente en escenarios 
             * con un gran número de usuarios y sesiones activas. */
            sessionRepository.Update(session);
            /*8. Probar exhaustivamente el proceso de refresco de tokens para garantizar su correcto funcionamiento y seguridad. */
            await unitOfWork.SaveChangesAsync(cancellationToken);
            /*9. Documentar claramente el proceso de refresco de tokens, incluyendo los requisitos de seguridad 
             * y las mejores prácticas para su implementación. */
            var response = new AuthResponseDto(
             jwtTokenService.CreateAccessToken(user),
             newRefreshToken,
             DateTimeOffset.UtcNow.AddMinutes(15),
             new UserSessionDto(user.Id, user.Email, user.FullName, user.IdInstitucion, user.Roles, user.Permissions));
            /* 10. Mantenerse actualizado con las últimas tendencias y vulnerabilidades en seguridad de autenticación y autorización,*/
            return ResponseFactory.Success(response);
        }
    }
    
}
