using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class CatSexoRepository : ICatSexoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatSexoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatSexo>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatSexo
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatSexo?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.CatSexo
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatSexo Sexo, CancellationToken cancellationToken)
        {
            await _context.CatSexo.AddAsync(
                Sexo,
                cancellationToken);
        }

        #endregion
    }
}