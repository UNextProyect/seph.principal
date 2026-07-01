using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatTipoPersonalRepository
    {
        Task<IReadOnlyList<CatTipoPersonal>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatTipoPersonal?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatTipoPersonal tipoPersonal, CancellationToken cancellationToken);

    }
}