using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;

namespace Seph.Principal.Application.Features.Users.Commands.CreateUser
{
    /// <summary>
    /// Un Admin crea un usuario normal. La IdInstitucion se inyecta desde el Admin autenticado
    /// (no la elige el cliente), garantizando que el usuario queda atado a la institución del Admin.
    /// </summary>
    public sealed record CreateUserCommand(
        string FullName,
        string Email,
        string Password,
        long IdInstitucion)
        : IRequest<ResponseWrapper<UserCreatedDto>>;
}
