namespace Seph.Principal.Application.Features.Auth.DTOs
{
    /*Transporta datos del usuario autenticado desde la capa de application hacia api 
     *o a otros casos de uso
     *el record presanta datos inmutables, es decir, una vez creado el objeto no se pueden modificar sus propiedades,
     *
     */
    public sealed record AuthenticatedUserDto(Guid Id, string Email, string FullName,
         IReadOnlyCollection<string> Roles,
         IReadOnlyCollection<string> Permissions,
         bool EmailConfirmed,
         long? IdInstitucion);


}
