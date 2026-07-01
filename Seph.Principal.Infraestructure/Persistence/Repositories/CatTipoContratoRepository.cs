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
    public sealed class CatTipoContratoRepository : ICatTipoContratoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor
        public CatTipoContratoRepository(
        ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region  Metodos de la clase
        public async Task<IReadOnlyList<CatTipoContrato>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CatTipoContratos
                .AsNoTracking()
                .OrderBy(x => x.StrValor)
                .ToListAsync(cancellationToken);
        }
        public async Task<CatTipoContrato?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CatTipoContratos
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(CatTipoContrato contrato, CancellationToken cancellationToken)
        {
            await _context.CatTipoContratos.AddAsync(
                contrato,
                cancellationToken);
        }

        #endregion
    }
}