using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; set; }
        string? Email { get; set; }

        string? IpAddress { get; set; }

        string? DeviceId { get; set; } 
    }
}
