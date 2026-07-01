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
    public sealed class CatTipoRepository : ICatTipoPersonalRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatTipoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatTipoPersonal>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatTipoPersonal
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatTipoPersonal?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatTipoPersonal
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatTipoPersonal personal, CancellationToken cancellationToken)
        {
            await _context.CatTipoPersonal.AddAsync(
                personal,
                cancellationToken);
        }

        #endregion
    }
}