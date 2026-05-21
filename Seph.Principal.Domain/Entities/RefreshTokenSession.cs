using Seph.Principal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    /*clase que */
    public class RefreshTokenSession: AuditableEntity
    {
        #region Constructors
        public RefreshTokenSession()
        {
                
        }

        private RefreshTokenSession(Guid userId, string tokenHash, string deviceId, string ipAddress, DateTimeOffset expiresAtUtc)
        {
            UserId = userId;
            TokenHash = tokenHash;
            DeviceId = deviceId;
            IpAddress = ipAddress;
            ExpiresAtUtc = expiresAtUtc;
            Status = SessionStatus.Active;
        }
        #endregion

        #region Factory Method
        public Guid UserId { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;
        public string DeviceId { get; private set; } = string.Empty;
        public string IpAddress { get; private set; } = string.Empty;
        public DateTimeOffset ExpiresAtUtc { get; private set; }
        public DateTimeOffset? RevokedAtUtc { get; private set; }
        public SessionStatus Status { get; private set; }

        public bool IsActive => Status == SessionStatus.Active && ExpiresAtUtc > DateTimeOffset.UtcNow;
        #endregion
    }
}
