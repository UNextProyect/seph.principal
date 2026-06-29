namespace Seph.Principal.Application.Features.Users.DTOs
{
    /// <summary>
    /// Datos del usuario recién creado (Admin o usuario normal) devueltos al cliente.
    /// </summary>
    public sealed record UserCreatedDto(
        Guid Id,
        string Email,
        string FullName,
        string Role,
        long? IdInstitucion);
}
