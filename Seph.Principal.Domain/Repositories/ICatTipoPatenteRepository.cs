using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatTipoPatenteRepository
    {
        Task<IReadOnlyList<CatTipoPatente>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatTipoPatente?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatTipoPatente patente, CancellationToken cancellationToken);

    }
}
