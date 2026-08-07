using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los reportes de patentes.
    /// </summary>
    public interface IReportePatenteRepository
    {
        /// <summary>
        /// Obtiene todos los reportes de patentes.
        /// </summary>
        Task<IReadOnlyList<ReportePatente>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene un reporte de patente
        /// por su identificador.
        /// </summary>
        Task<ReportePatente?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene las patentes asociadas a una institución
        /// dentro de un periodo específico.
        /// </summary>
        Task<IReadOnlyList<ReportePatente>> GetByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene una patente con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        Task<ReportePatente?> GetByIdForUpdateAsync(
            long id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Verifica si ya existe una patente con el número
        /// de registro o solicitud indicado.
        /// </summary>
        Task<bool> ExistsByNumeroRegistroSolicitudAsync(
            string strNumeroRegistroSolicitud,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo reporte de patente.
        /// </summary>
        Task AddAsync(
            ReportePatente reportePatente,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene las patentes del periodo anterior
        /// correspondientes a una institución.
        /// </summary>
        Task<IReadOnlyList<ReportePatente>> GetPreviousReportesAsync(
            long idInstitucion,
            int intAnio,
            int intNumeroPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un reporte de patente para actualización.
        /// </summary>
        void Update(ReportePatente reportePatente);

        /// <summary>
        /// Marca un reporte de patente para eliminación.
        /// </summary>
        void Delete(ReportePatente reportePatente);
    }
}