using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos
    /// para las respuestas de un análisis estratégico.
    /// </summary>
    public sealed class RespuestaAnalisisRepository
        : IRespuestaAnalisisRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando
        /// el contexto principal de la aplicación.
        /// </summary>
        public RespuestaAnalisisRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todas las respuestas asociadas
        /// a un reporte de análisis estratégico.
        /// </summary>
        public async Task<IReadOnlyList<RespuestaAnalisis>>
            GetByIdAnalisisEstrategicoAsync(
                long idAnalisisEstrategico,
                CancellationToken cancellationToken)
        {
            return await _context.RespuestasAnalisis
                .AsNoTracking()
                .Where(x =>
                    x.IdAnalisisEstrategico
                        == idAnalisisEstrategico)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene las respuestas con seguimiento habilitado
        /// para realizar una actualización.
        /// </summary>
        public async Task<IReadOnlyList<RespuestaAnalisis>>
            GetByIdAnalisisEstrategicoForUpdateAsync(
                long idAnalisisEstrategico,
                CancellationToken cancellationToken)
        {
            return await _context.RespuestasAnalisis
                .Where(x =>
                    x.IdAnalisisEstrategico
                        == idAnalisisEstrategico)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Verifica si una pregunta ya tiene
        /// una respuesta dentro del análisis.
        /// </summary>
        public async Task<bool> ExistsByAnalisisPreguntaAsync(
            long idAnalisisEstrategico,
            long idPreguntaAnalisis,
            CancellationToken cancellationToken)
        {
            return await _context.RespuestasAnalisis
                .AsNoTracking()
                .AnyAsync(
                    x =>
                        x.IdAnalisisEstrategico
                            == idAnalisisEstrategico
                        && x.IdPreguntaAnalisis
                            == idPreguntaAnalisis,
                    cancellationToken);
        }

        /// <summary>
        /// Agrega una nueva respuesta
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            RespuestaAnalisis respuestaAnalisis,
            CancellationToken cancellationToken)
        {
            await _context.RespuestasAnalisis.AddAsync(
                respuestaAnalisis,
                cancellationToken);
        }

        /// <summary>
        /// Agrega una colección de respuestas
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddRangeAsync(
            IEnumerable<RespuestaAnalisis> respuestasAnalisis,
            CancellationToken cancellationToken)
        {
            await _context.RespuestasAnalisis.AddRangeAsync(
                respuestasAnalisis,
                cancellationToken);
        }

        /// <summary>
        /// Marca una respuesta para que
        /// sus cambios sean actualizados.
        /// </summary>
        public void Update(
            RespuestaAnalisis respuestaAnalisis)
        {
            _context.RespuestasAnalisis.Update(
                respuestaAnalisis);
        }

        #endregion
    }
}
