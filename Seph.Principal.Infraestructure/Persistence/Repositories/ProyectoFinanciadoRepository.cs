using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los proyectos financiados de un reporte.
    /// </summary>
    public sealed class ProyectoFinanciadoRepository
        : IProyectoFinanciadoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public ProyectoFinanciadoRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los proyectos asociados
        /// a un reporte financiero.
        /// </summary>
        public async Task<IReadOnlyList<ProyectoFinanciado>>
            GetByIdReporteFinanzaAsync(
                long idReporteFinanza,
                CancellationToken cancellationToken)
        {
            return await _context.ProyectoFinanciados
                .AsNoTracking()
                .Where(x => x.IdReporteFinanza == idReporteFinanza)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo proyecto financiado
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            ProyectoFinanciado proyectoFinanciado,
            CancellationToken cancellationToken)
        {
            await _context.ProyectoFinanciados.AddAsync(
                proyectoFinanciado,
                cancellationToken);
        }

        /// <summary>
        /// Agrega una colección de proyectos
        /// financiados al contexto de la aplicación.
        /// </summary>
        public async Task AddRangeAsync(
            IEnumerable<ProyectoFinanciado> proyectosFinanciados,
            CancellationToken cancellationToken)
        {
            await _context.ProyectoFinanciados.AddRangeAsync(
                proyectosFinanciados,
                cancellationToken);
        }

        /// <summary>
        /// Elimina todos los proyectos asociados
        /// a un reporte financiero.
        /// </summary>
        public async Task DeleteByIdReporteFinanzaAsync(
            long idReporteFinanza,
            CancellationToken cancellationToken)
        {
            var registros = await _context.ProyectoFinanciados
                .Where(x => x.IdReporteFinanza == idReporteFinanza)
                .ToListAsync(cancellationToken);

            _context.ProyectoFinanciados.RemoveRange(registros);
        }

        /// <summary>
        /// Marca un proyecto financiado
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            ProyectoFinanciado proyectoFinanciado)
        {
            _context.ProyectoFinanciados.Remove(
                proyectoFinanciado);
        }

        #endregion
    }
}