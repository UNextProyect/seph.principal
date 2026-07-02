namespace Seph.Principal.Application.Features.Auth.DTOs
{
    public sealed record UserSessionDto(Guid Id, string Email, string FullName, long? IdInstitucion, IReadOnlyCollection<string> Roles,
    IReadOnlyCollection<string> Permissions);
    
    
}
