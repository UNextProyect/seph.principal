using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Application.Common.Interfaces
{
    /*Esta interfaz define el contrato para la capa de acceso a datos de la aplicación.*/
    public interface IApplicationDbContext
    {
        DbSet<RefreshTokenSession> RefreshTokenSessions { get; }
        DbSet<HistorialContrato> HistorialContratos { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}
