using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface ICatTipoContratoRepository
    {
        Task<IReadOnlyList<CatTipoContrato>> GetAllAsync(CancellationToken cancellationToken);

        Task<CatTipoContrato?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(CatTipoContrato tipoContrato, CancellationToken cancellationToken);

    }
}
