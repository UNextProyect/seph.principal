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
    public sealed class CatAreaRepository : ICatAreaRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatAreaRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatArea>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatAreas
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatArea?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatAreas
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatArea area, CancellationToken cancellationToken)
        {
            await _context.CatAreas.AddAsync(
                area,
                cancellationToken);
        }

        #endregion
    }
}