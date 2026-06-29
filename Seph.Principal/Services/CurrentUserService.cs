using Seph.Principal.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Seph.Principal.Services
{
    /*public interface IJwtService*/
    public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid? UserId
        {
            get
            {
                var value = httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                    ?? httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(value, out var userId) ? userId : null;
            }
        }

        public string? Email => httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Email);
        public string? IpAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        public string? DeviceId => httpContextAccessor.HttpContext?.Request.Headers["X-Device-Id"].FirstOrDefault();

        public long? IdInstitucion
        {
            get
            {
                var value = httpContextAccessor.HttpContext?.User.FindFirstValue("idInstitucion");
                return long.TryParse(value, out var id) ? id : null;
            }
        }
    }
}
