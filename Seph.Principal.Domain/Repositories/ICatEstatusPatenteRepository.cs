using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatEstatusPatenteRepository
    {
        Task<IReadOnlyList<CatEstatusPatente>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatEstatusPatente?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatEstatusPatente tipoPatente, CancellationToken cancellationToken);

    }
}
