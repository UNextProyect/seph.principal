using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Features.Auth.DTOs;

namespace Seph.Principal.Infraestructure.Identity
{
    public sealed class IdentityService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : IIdentityService
    {
        public async Task<AuthenticatedUserDto> ValidateCredentialAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(
                candidate => candidate.NormalizedEmail == email.ToUpperInvariant(),cancellationToken);
            if (user is null || !user.IsActive)
            {
                return null;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            return result.Succeeded ? await MapUserAsync(user) : null;
        }


        /* El método GetUserByIdAsync es un método público que se encarga de obtener la información de un usuario
         * autenticado a partir de su identificador (userId).*/
        public async Task<AuthenticatedUserDto?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(candidate => candidate.Id == userId, cancellationToken);
            return user is null || !user.IsActive ? null : await MapUserAsync(user);
        }

        /* El método GetUserPermissionsAsync es un método público que se encarga de obtener los permisos de un usuario a partir de su identificador (userId).
         * 
         */
        public async Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(candidate => candidate.Id == userId, cancellationToken);
            if (user is null)
            {
                return [];
            }

            var claims = await userManager.GetClaimsAsync(user);
            return claims.Where(claim => claim.Type == "permission").Select(claim => claim.Value).Distinct().ToArray();
        }
        /* El método MarkLastLoginAsync es un método público que se encarga de actualizar la fecha 
         * y hora del último inicio de sesión de un usuario. 
         */
        public async Task MarkLastLoginAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(candidate => candidate.Id == userId, cancellationToken);
            if (user is null)
            {
                return;
            }

            user.LastLoginAtUtc = DateTimeOffset.UtcNow;
            await userManager.UpdateAsync(user);
        }


        /* El método MapUserAsync es un método privado que se encarga de mapear un objeto ApplicationUser a un objeto AuthenticatedUserDto. 
         * Este método obtiene los roles y permisos del usuario utilizando el UserManager, y luego crea una instancia 
         * de AuthenticatedUserDto con la información del usuario, sus roles y permisos.
         * Este método es utilizado en el método ValidateCredentialAsync para devolver 
         * la información del usuario autenticado si las credenciales son válidas.
         */
        private async Task<AuthenticatedUserDto> MapUserAsync(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = await userManager.GetClaimsAsync(user);
            var permissions = claims.Where(claim => claim.Type == "permission").Select(claim => claim.Value).Distinct().ToArray();

            return new AuthenticatedUserDto(user.Id, user.Email ?? string.Empty, user.FullName, roles.ToList(), permissions);
        }

    }
}
