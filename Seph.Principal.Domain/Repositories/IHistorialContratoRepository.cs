using Seph.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Repositories
{
    public interface IHistorialContratoRepository
    {
        Task<IReadOnlyList<HistorialContrato>> GetAllAsync(CancellationToken cancellationToken);

        Task<HistorialContrato?> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task AddAsync(HistorialContrato historialContrato, CancellationToken cancellationToken);

        void Update(HistorialContrato historialContrato);

        void Delete(HistorialContrato historialContrato);
    }
}