using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Users.DTOs;

namespace Seph.Principal.Application.Features.Users.Commands.CreateAdmin
{
    /// <summary>
    /// El SuperAdmin crea un Admin atado a una institución (IdInstitucion).
    /// </summary>
    public sealed record CreateAdminCommand(
        string FullName,
        string Email,
        string Password,
        long IdInstitucion)
        : IRequest<ResponseWrapper<UserCreatedDto>>;
}
