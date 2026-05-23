using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;

namespace Seph.Principal.Application.Features.Users.Queries.GetCurrentUser
{
    /*Este código define un manejador de consultas para obtener la información del usuario actual.*/
    public sealed class GetCurrentUserQueryHandler(IIdentityService identityService)
        : IRequestHandler<GetCurrentUserQuery,ResponseWrapper<UserSessionDto>>
    {
        /*El método Handle es el encargado de procesar la consulta GetCurrentUserQuery.*/
        public async Task<ResponseWrapper<UserSessionDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.UserId,cancellationToken);
            if (user is null)
            {
                return ResponseFactory.Failure<UserSessionDto>("Usuario no encontrado",System.Net.HttpStatusCode.NotFound)!;
            }
            /*Si el usuario es encontrado, se crea un objeto UserSessionDto con la información del usuario
             * y se devuelve una respuesta exitosa con ese objeto como datos.*/
            return ResponseFactory.Success(new UserSessionDto(

                user.Id,
                user.Email,
                user.FullName,
                user.Roles,
                user.Permissions), "Usuario actual obtenido correctamente");
            
        }
    }
}
