using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los proyectos financiados de un reporte.
    /// </summary>
    public interface IProyectoFinanciadoRepository
    {
        /// <summary>
        /// Obtiene todos los proyectos asociados
        /// a un reporte financiero.
        /// </summary>
        Task<IReadOnlyList<ProyectoFinanciado>> GetByIdReporteFinanzaAsync(
            long idReporteFinanza,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo proyecto financiado
        /// al reporte correspondiente.
        /// </summary>
        Task AddAsync(
            ProyectoFinanciado proyectoFinanciado,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega una colección de proyectos
        /// financiados al reporte correspondiente.
        /// </summary>
        Task AddRangeAsync(
            IEnumerable<ProyectoFinanciado> proyectosFinanciados,
            CancellationToken cancellationToken);

        /// <summary>
        /// Elimina todos los proyectos asociados
        /// a un reporte financiero.
        /// </summary>
        Task DeleteByIdReporteFinanzaAsync(
            long idReporteFinanza,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un proyecto financiado
        /// para eliminación.
        /// </summary>
        void Delete(
            ProyectoFinanciado proyectoFinanciado);
    }
}