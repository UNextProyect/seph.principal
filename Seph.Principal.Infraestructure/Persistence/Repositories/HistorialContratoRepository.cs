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
    public class HistorialContratoRepository : IHistorialContratoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public HistorialContratoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodos de la clase

        public async Task<IReadOnlyList<HistorialContrato>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.HistorialContratos
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<HistorialContrato?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.HistorialContratos
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AddAsync(HistorialContrato historialContrato, CancellationToken cancellationToken)
        {
            await _context.HistorialContratos.AddAsync(
                historialContrato,
                cancellationToken);
        }

        public void Update(HistorialContrato historialContrato)
        {
            _context.HistorialContratos.Update(historialContrato);
        }

        public void Delete(HistorialContrato historialContrato)
        {
            _context.HistorialContratos.Remove(historialContrato);
        }

        #endregion
    }
}