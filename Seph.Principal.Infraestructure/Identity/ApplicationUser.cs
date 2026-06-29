using Microsoft.AspNetCore.Identity;

namespace Seph.Principal.Infraestructure.Identity
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTimeOffset? LastLoginAtUtc { get; set; }

        /// <summary>
        /// Institución a la que pertenecen los usuarios.
        /// NULL para el SuperAdmin, que no está atado a una institución.
        /// </summary>
        public long? IdInstitucion { get; set; }
    }
}
