using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para los reportes de patentes.
    /// </summary>
    public sealed class ReportePatenteRepository
        : IReportePatenteRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando el contexto
        /// principal de la aplicación.
        /// </summary>
        public ReportePatenteRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los reportes de patentes
        /// sin habilitar el seguimiento de cambios.
        /// </summary>
        public async Task<IReadOnlyList<ReportePatente>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.ReportePatentes
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte de patente
        /// mediante su identificador.
        /// </summary>
        public async Task<ReportePatente?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePatentes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene las patentes correspondientes a una relación
        /// entre institución y periodo.
        /// </summary>
        public async Task<IReadOnlyList<ReportePatente>>
            GetByMapInstitucionPeriodoAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReportePatentes
                .AsNoTracking()
                .Where(
                    x => x.IdMapInstitucionPeriodo ==
                        idMapInstitucionPeriodo)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene una patente con seguimiento habilitado
        /// para permitir su actualización.
        /// </summary>
        public async Task<ReportePatente?> GetByIdForUpdateAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePatentes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /// <summary>
        /// Verifica si ya existe una patente con el número
        /// de registro o solicitud indicado.
        /// </summary>
        public async Task<bool> ExistsByNumeroRegistroSolicitudAsync(
            string strNumeroRegistroSolicitud,
            CancellationToken cancellationToken)
        {
            return await _context.ReportePatentes
                .AnyAsync(
                    x => x.StrNumeroRegistroSolicitud ==
                        strNumeroRegistroSolicitud,
                    cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo reporte de patente
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            ReportePatente reportePatente,
            CancellationToken cancellationToken)
        {
            await _context.ReportePatentes.AddAsync(
                reportePatente,
                cancellationToken);
        }

        /// <summary>
        /// Obtiene las patentes pertenecientes al periodo
        /// anterior más reciente de una institución.
        /// </summary>
        public async Task<IReadOnlyList<ReportePatente>>
            GetPreviousReportesAsync(
                long idInstitucion,
                int intAnio,
                int intNumeroPeriodo,
                CancellationToken cancellationToken)
        {
            var idMapInstitucionPeriodoAnterior =
                await _context.MapInstitucionPeriodos
                    .AsNoTracking()
                    .Join(
                        _context.CatPeriodos,
                        map => map.IdPeriodo,
                        periodo => periodo.Id,
                        (map, periodo) => new
                        {
                            map,
                            periodo
                        })
                    .Where(x =>
                        x.map.IdInstitucion == idInstitucion &&
                        (
                            x.periodo.IntAnio < intAnio ||
                            x.periodo.IntAnio == intAnio &&
                            x.periodo.IntNumeroPeriodo <
                            intNumeroPeriodo
                        ))
                    .OrderByDescending(x => x.periodo.IntAnio)
                    .ThenByDescending(
                        x => x.periodo.IntNumeroPeriodo)
                    .Select(x => (long?)x.map.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (!idMapInstitucionPeriodoAnterior.HasValue)
            {
                return Array.Empty<ReportePatente>();
            }

            return await _context.ReportePatentes
                .AsNoTracking()
                .Where(
                    x => x.IdMapInstitucionPeriodo ==
                        idMapInstitucionPeriodoAnterior.Value)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Marca un reporte de patente
        /// para que sus cambios sean actualizados.
        /// </summary>
        public void Update(
            ReportePatente reportePatente)
        {
            _context.ReportePatentes.Update(
                reportePatente);
        }

        /// <summary>
        /// Marca un reporte de patente
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            ReportePatente reportePatente)
        {
            _context.ReportePatentes.Remove(
                reportePatente);
        }

        #endregion
    }
}