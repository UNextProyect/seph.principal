namespace Seph.Principal.Application.Features.Auth.DTOs
{
    public sealed record AuthResponseDto(
        string AccessToken,
        string RefreshToken,
        DateTimeOffset ExpiresAtUtc,
        UserSessionDto User);
}
