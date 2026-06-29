namespace Seph.Principal.Application.Features.Users.DTOs
{
    /// <summary>
    /// Cuerpo de la petición para que el SuperAdmin cree un Admin de institución.
    /// </summary>
    public sealed record CreateAdminRequest(
        string FullName,
        string Email,
        string Password,
        long IdInstitucion);
}
