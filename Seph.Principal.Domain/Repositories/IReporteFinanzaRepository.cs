using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los reportes financieros.
    /// </summary>
    public interface IReporteFinanzaRepository
    {
        /// <summary>
        /// Obtiene todos los reportes financieros.
        /// </summary>
        Task<IReadOnlyList<ReporteFinanza>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene un reporte financiero por su identificador.
        /// </summary>
        Task<ReporteFinanza?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte asociado a una institución
        /// dentro de un periodo específico.
        /// </summary>
        Task<ReporteFinanza?> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        Task<ReporteFinanza?> GetByMapInstitucionPeriodoForUpdateAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Verifica si ya existe un reporte para la relación
        /// entre institución y periodo indicada.
        /// </summary>
        Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo reporte financiero.
        /// </summary>
        Task AddAsync(
            ReporteFinanza reporteFinanza,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte del periodo anterior
        /// correspondiente a una institución.
        /// </summary>
        Task<ReporteFinanza?> GetPreviousReporteAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un reporte financiero para actualización.
        /// </summary>
        void Update(ReporteFinanza reporteFinanza);

        /// <summary>
        /// Marca un reporte financiero para eliminación.
        /// </summary>
        void Delete(ReporteFinanza reporteFinanza);
    }
}