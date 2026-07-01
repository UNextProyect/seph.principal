using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatAreaRepository
    {
        Task<IReadOnlyList<CatArea>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatArea?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatArea area, CancellationToken cancellationToken);

    }
}
