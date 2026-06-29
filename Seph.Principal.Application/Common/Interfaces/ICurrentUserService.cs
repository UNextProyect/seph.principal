namespace Seph.Principal.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        string? IpAddress { get; }
        string? DeviceId { get; }
        long? IdInstitucion { get; }
    }
}
