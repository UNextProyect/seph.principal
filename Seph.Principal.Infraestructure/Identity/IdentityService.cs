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
                candidate => candidate.NormalizedEmail == email.ToUpperInvariant(), cancellationToken);
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
            var user = await userManager.Users
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
            {
                return;
            }

            user.LastLoginAtUtc = DateTimeOffset.UtcNow;

            try
            {
                await userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== ERROR COMPLETO ===");
                Console.WriteLine(ex.ToString());
                throw;
            }
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

            return new AuthenticatedUserDto(user.Id, user.Email ?? string.Empty, user.FullName, roles.ToList(), permissions, user.EmailConfirmed, user.IdInstitucion);
        }

        /// <summary>
        /// El método FindOrCreateGoogleUserAsync es un método público que se encarga de encontrar o crear un usuario a partir de la información proporcionada por Google.
        /// Este método busca un usuario en la base de datos utilizando el correo electrónico proporcionado por Google.
        /// Si no encuentra un usuario, crea uno nuevo con la información proporcionada (correo electrónico, nombre completo y ID de Google). 
        /// Luego, asigna un rol por defecto ("User") al nuevo usuario y devuelve la información del usuario autenticado utilizando el método MapUserAsync.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="fullName"></param>
        /// <param name="googleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AuthenticatedUserDto> FindOrCreateGoogleUserAsync(string email, string fullName, string googleId, CancellationToken cancellationToken)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpperInvariant(), cancellationToken);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = fullName,
                    IsActive = true,
                    EmailConfirmed = true,  // Google ya verificó el email
                };
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded) return null;

                // Asigna un rol por defecto en user
                await userManager.AddToRoleAsync(user, "User");
            }

            return await MapUserAsync(user);
        }

        public async Task<Guid?> RegisterAsync(string fullName, string email, string password, CancellationToken cancellationToken)
        {
            var existing = await userManager.FindByEmailAsync(email);
            if (existing is not null) return null;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                IsActive = true,
                EmailConfirmed = false
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await userManager.AddToRoleAsync(user, "User");

            return user.Id;
        }

        public async Task<Guid?> CreateUserWithRoleAsync(string fullName, string email, string password, string role, long? idInstitucion, CancellationToken cancellationToken)
        {
            var existing = await userManager.FindByEmailAsync(email);
            if (existing is not null) return null;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                IsActive = true,
                EmailConfirmed = true,        // creado por un administrador: se da por verificado
                IdInstitucion = idInstitucion
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await userManager.AddToRoleAsync(user, role);

            return user.Id;
        }

        /// <summary>
        /// El método GetUserIdByEmailAsync es un método público que se encarga de obtener el identificador de un usuario a partir de su correo electrónico.
        /// Este método busca un usuario en la base de datos utilizando el correo electrónico proporcionado y devuelve su ID si lo encuentra o null si no lo encuentra.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid?> GetUserIdByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(
                candidate => candidate.NormalizedEmail == email.ToUpperInvariant(), cancellationToken);
            return user?.Id;
        }

        public async Task<bool> IsEmailConfirmedAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(
                candidate => candidate.Id == userId, cancellationToken);
            return user?.EmailConfirmed ?? false;
        }

        public async Task<bool> ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(
                candidate => candidate.Id == userId, cancellationToken);
            if (user is null) return false;

            user.EmailConfirmed = true;
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
