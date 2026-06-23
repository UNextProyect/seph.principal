using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatSexoRepository
    {
        Task<IReadOnlyList<CatSexo>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatSexo?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task AddAsync(CatSexo sexo, CancellationToken cancellationToken);

    }
}
