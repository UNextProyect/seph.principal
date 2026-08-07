using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los inventores de una patente.
    /// </summary>
    public sealed class InventorPatenteRepository
        : IInventorPatenteRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public InventorPatenteRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los inventores asociados
        /// a un reporte de patente.
        /// </summary>
        public async Task<IReadOnlyList<InventorPatente>>
            GetByIdPatenteAsync(
                long idPatente,
                CancellationToken cancellationToken)
        {
            return await _context.InventoresPatente
                .AsNoTracking()
                .Where(x => x.IdPatente == idPatente)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo inventor
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            InventorPatente inventorPatente,
            CancellationToken cancellationToken)
        {
            await _context.InventoresPatente.AddAsync(
                inventorPatente,
                cancellationToken);
        }

        /// <summary>
        /// Agrega una colección de inventores
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddRangeAsync(
            IEnumerable<InventorPatente> inventoresPatente,
            CancellationToken cancellationToken)
        {
            await _context.InventoresPatente.AddRangeAsync(
                inventoresPatente,
                cancellationToken);
        }

        /// <summary>
        /// Elimina todos los inventores asociados
        /// a un reporte de patente.
        /// </summary>
        public async Task DeleteByIdPatenteAsync(
            long idPatente,
            CancellationToken cancellationToken)
        {
            var registros = await _context.InventoresPatente
                .Where(x => x.IdPatente == idPatente)
                .ToListAsync(cancellationToken);

            _context.InventoresPatente.RemoveRange(registros);
        }

        /// <summary>
        /// Marca un inventor
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            InventorPatente inventorPatente)
        {
            _context.InventoresPatente.Remove(
                inventorPatente);
        }

        #endregion
    }
}