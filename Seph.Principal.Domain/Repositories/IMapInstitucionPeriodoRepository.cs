using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IMapInstitucionPeriodoRepository
    {
        Task<IReadOnlyList<MapInstitucionPeriodo>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<MapInstitucionPeriodo>> GetByInstitucionAsync(
        long idInstitucion,
        CancellationToken cancellationToken);

       Task<MapInstitucionPeriodo?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<MapInstitucionPeriodo?> GetByInstitucionPeriodoAsync(
            long idInstitucion,
            long idPeriodo,
            CancellationToken cancellationToken);

        Task AddAsync(MapInstitucionPeriodo mapInstitucionPeriodo, CancellationToken cancellationToken);
        Task<MapInstitucionPeriodo?> GetPeriodoActivoByInstitucionAsync(
        long idInstitucion,
        CancellationToken cancellationToken);

        void Update(MapInstitucionPeriodo mapInstitucionPeriodo);

        void Delete(MapInstitucionPeriodo mapInstitucionPeriodo);
    }
}
