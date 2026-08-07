using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatTipoPatenteRepository : ICatTipoPatenteRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatTipoPatenteRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatTipoPatente>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatTipoPatentes
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatTipoPatente?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatTipoPatentes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatTipoPatente tipoPatente, CancellationToken cancellationToken)
        {
            await _context.CatTipoPatentes.AddAsync(
                tipoPatente,
                cancellationToken);
        }

        #endregion
    }
}