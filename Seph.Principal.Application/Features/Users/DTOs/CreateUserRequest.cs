namespace Seph.Principal.Application.Features.Users.DTOs
{
    /// <summary>
    /// Cuerpo de la petición para que un Admin cree un usuario normal.
    /// La institución NO viene en el cuerpo: se toma de la institución del Admin autenticado.
    /// </summary>
    public sealed record CreateUserRequest(
        string FullName,
        string Email,
        string Password);
}
