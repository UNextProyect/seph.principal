using Seph.Principal.Application.Features.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticatedUserDto> ValidateCredentialAsync(string email, string password, CancellationToken cancellationToken);
        Task<AuthenticatedUserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);
        Task<Guid?> RegisterAsync(string fullName, string email, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Crea un usuario con un rol específico y una institución asignada.
        /// Usado por el SuperAdmin para crear Admins y por los Admins para crear usuarios de su institución.
        /// Devuelve el Id del usuario creado, o null si el correo ya existe o falla la creación.
        /// </summary>
        Task<Guid?> CreateUserWithRoleAsync(string fullName, string email, string password, string role, long? idInstitucion, CancellationToken cancellationToken);
        Task MarkLastLoginAsync(Guid userId, CancellationToken cancellationToken);
        Task<AuthenticatedUserDto> FindOrCreateGoogleUserAsync(string email, string fullName, string googleId, CancellationToken cancellationToken);
        /// <summary>
        /// Metodo para obtener el id del usuario a partir de su correo electrónico,
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid?> GetUserIdByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Metodo para verificar si el correo electrónico de un usuario específico ha sido confirmado,
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsEmailConfirmedAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Metodo para marcar el correo electrónico de un usuario específico como confirmado,
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken);
    }
}
