using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface IInstitucionRepository
    {
        Task<IReadOnlyList<Institucion>> GetAllAsync(CancellationToken cancellationToken);

        Task<Institucion?> GetByIdAsync(int id,CancellationToken cancellationToken);

        Task AddAsync(Institucion institucion,CancellationToken cancellationToken);

        void Update(Institucion institucion);

        void Delete(Institucion institucion);
    }
}
