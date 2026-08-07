using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatEstatusPatenteRepository : ICatEstatusPatenteRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatEstatusPatenteRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatEstatusPatente>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatEstatusPatentes
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatEstatusPatente?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatEstatusPatentes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatEstatusPatente estatusPatente, CancellationToken cancellationToken)
        {
            await _context.CatEstatusPatentes.AddAsync(
                estatusPatente,
                cancellationToken);
        }

        #endregion
    }
}