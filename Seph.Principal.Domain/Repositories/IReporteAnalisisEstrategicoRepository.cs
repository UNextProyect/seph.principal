using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los reportes de análisis estratégico.
    /// </summary>
    public interface IReporteAnalisisEstrategicoRepository
    {
        /// <summary>
        /// Obtiene todos los reportes
        /// de análisis estratégico.
        /// </summary>
        Task<IReadOnlyList<ReporteAnalisisEstrategico>> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene un reporte por su identificador.
        /// </summary>
        Task<ReporteAnalisisEstrategico?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte asociado a una relación
        /// entre institución y periodo.
        /// </summary>
        Task<ReporteAnalisisEstrategico?>
            GetByMapInstitucionPeriodoAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene el reporte con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        Task<ReporteAnalisisEstrategico?>
            GetByMapInstitucionPeriodoForUpdateAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken);

        /// <summary>
        /// Verifica si ya existe un reporte
        /// para la relación institución-periodo.
        /// </summary>
        Task<bool> ExistsByMapInstitucionPeriodoAsync(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo reporte
        /// de análisis estratégico.
        /// </summary>
        Task AddAsync(
            ReporteAnalisisEstrategico reporteAnalisisEstrategico,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca el reporte para actualización.
        /// </summary>
        void Update(
            ReporteAnalisisEstrategico reporteAnalisisEstrategico);

        /// <summary>
        /// Marca el reporte para eliminación.
        /// </summary>
        void Delete(
            ReporteAnalisisEstrategico reporteAnalisisEstrategico);
    }
}
