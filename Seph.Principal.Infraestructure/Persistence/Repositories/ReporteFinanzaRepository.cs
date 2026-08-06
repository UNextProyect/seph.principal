using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los reportes financieros.
    /// </summary>
    public sealed class ReporteFinanzaRepository
        : IReporteFinanzaRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public ReporteFinanzaRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los reportes financieros
        /// sin habilitar el seguimiento de cambios.
        /// </summary>
        public async Task<IReadOnlyList<ReporteFinanza>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.ReporteFinanzas
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte financiero
        /// mediante su identificador.
        /// </summary>
        public async Task<ReporteFinanza?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteFinanzas
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte correspondiente a una relación
        /// entre institución y periodo.
        /// </summary>
        public async Task<ReporteFinanza?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteFinanzas
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte con seguimiento habilitado
        /// para permitir su actualización.
        /// </summary>
        public async Task<ReporteFinanza?>
            GetByMapInstitucionPeriodoForUpdateAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReporteFinanzas
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Verifica si ya existe un reporte para la relación
        /// entre institución y periodo indicada.
        /// </summary>
        public async Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteFinanzas
                .AnyAsync(
                    x => x.IdMapInstitucionPeriodo == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo reporte financiero
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            ReporteFinanza reporteFinanza,
            CancellationToken cancellationToken)
        {
            await _context.ReporteFinanzas.AddAsync(
                reporteFinanza,
                cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte financiero más reciente
        /// perteneciente a un periodo anterior.
        /// </summary>
        public async Task<ReporteFinanza?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteFinanzas
                .AsNoTracking()
                .Join(
                    _context.MapInstitucionPeriodos,
                    reporte => reporte.IdMapInstitucionPeriodo,
                    map => map.Id,
                    (reporte, map) => new
                    {
                        reporte,
                        map
                    })
                .Join(
                    _context.CatPeriodos,
                    reporteMap => reporteMap.map.IdPeriodo,
                    periodo => periodo.Id,
                    (reporteMap, periodo) => new
                    {
                        reporteMap.reporte,
                        reporteMap.map,
                        periodo
                    })
                .Where(x =>
                    x.map.IdInstitucion == idInstitucion &&
                    (
                        x.periodo.IntAnio < intAnio ||
                        x.periodo.IntAnio == intAnio &&
                        x.periodo.IntNumeroPeriodo < intNumeroPeriodo
                    ))
                .OrderByDescending(x => x.periodo.IntAnio)
                .ThenByDescending(x => x.periodo.IntNumeroPeriodo)
                .Select(x => x.reporte)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Marca un reporte financiero
        /// para que sus cambios sean actualizados.
        /// </summary>
        public void Update(
            ReporteFinanza reporteFinanza)
        {
            _context.ReporteFinanzas.Update(
                reporteFinanza);
        }

        /// <summary>
        /// Marca un reporte financiero
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            ReporteFinanza reporteFinanza)
        {
            _context.ReporteFinanzas.Remove(
                reporteFinanza);
        }

        #endregion
    }
}