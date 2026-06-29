using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class InstitucionRepository : IInstitucionRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public InstitucionRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<Institucion>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Instituciones
                .AsNoTracking()
                .OrderBy(x => x.StrNombre)
                .ToListAsync(cancellationToken);
        }
        public async Task<Institucion?> GetByIdAsync(long id,CancellationToken cancellationToken)
        {
            return await _context.Instituciones
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(Institucion institucion,CancellationToken cancellationToken)
        {
            await _context.Instituciones.AddAsync(
                institucion,
                cancellationToken);
        }

        public void Update(Institucion institucion)
        {
            _context.Instituciones.Update(institucion);
        }

        public void Delete(Institucion institucion)
        {
            _context.Instituciones.Remove(institucion);
        }


        #endregion

    }
}
