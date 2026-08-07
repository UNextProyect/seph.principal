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
    /// para las respuestas de un análisis estratégico.
    /// </summary>
    public interface IRespuestaAnalisisRepository
    {
        /// <summary>
        /// Obtiene todas las respuestas asociadas
        /// a un reporte de análisis estratégico.
        /// </summary>
        Task<IReadOnlyList<RespuestaAnalisis>>
            GetByIdAnalisisEstrategicoAsync(
                long idAnalisisEstrategico,
                CancellationToken cancellationToken);

        /// <summary>
        /// Obtiene las respuestas con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        Task<IReadOnlyList<RespuestaAnalisis>>
            GetByIdAnalisisEstrategicoForUpdateAsync(
                long idAnalisisEstrategico,
                CancellationToken cancellationToken);

        /// <summary>
        /// Verifica si una pregunta ya tiene
        /// una respuesta dentro del análisis.
        /// </summary>
        Task<bool> ExistsByAnalisisPreguntaAsync(
            long idAnalisisEstrategico,
            long idPreguntaAnalisis,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega una nueva respuesta
        /// al análisis estratégico.
        /// </summary>
        Task AddAsync(
            RespuestaAnalisis respuestaAnalisis,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega una colección de respuestas
        /// al análisis estratégico.
        /// </summary>
        Task AddRangeAsync(
            IEnumerable<RespuestaAnalisis> respuestasAnalisis,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca una respuesta
        /// para actualización.
        /// </summary>
        void Update(
            RespuestaAnalisis respuestaAnalisis);
    }
}
