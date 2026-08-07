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
    /// para los reportes de análisis estratégico.
    /// </summary>
    public sealed class ReporteAnalisisEstrategicoRepository
        : IReporteAnalisisEstrategicoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        /// <summary>
        /// Inicializa el repositorio utilizando
        /// el contexto principal de la aplicación.
        /// </summary>
        public ReporteAnalisisEstrategicoRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /// <summary>
        /// Obtiene todos los reportes de análisis estratégico
        /// sin habilitar el seguimiento de cambios.
        /// </summary>
        public async Task<
            IReadOnlyList<ReporteAnalisisEstrategico>> GetAllAsync(
                CancellationToken cancellationToken)
        {
            return await _context.ReporteAnalisisEstrategicos
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtiene un reporte de análisis estratégico
        /// mediante su identificador.
        /// </summary>
        public async Task<ReporteAnalisisEstrategico?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.ReporteAnalisisEstrategicos
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte correspondiente a una relación
        /// entre institución y periodo.
        /// </summary>
        public async Task<ReporteAnalisisEstrategico?>
            GetByMapInstitucionPeriodoAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReporteAnalisisEstrategicos
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo
                        == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Obtiene el reporte con seguimiento habilitado
        /// para permitir su actualización.
        /// </summary>
        public async Task<ReporteAnalisisEstrategico?>
            GetByMapInstitucionPeriodoForUpdateAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReporteAnalisisEstrategicos
                .FirstOrDefaultAsync(
                    x => x.IdMapInstitucionPeriodo
                        == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Verifica si ya existe un reporte para la relación
        /// entre institución y periodo indicada.
        /// </summary>
        public async Task<bool>
            ExistsByMapInstitucionPeriodoAsync(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            return await _context.ReporteAnalisisEstrategicos
                .AsNoTracking()
                .AnyAsync(
                    x => x.IdMapInstitucionPeriodo
                        == idMapInstitucionPeriodo,
                    cancellationToken);
        }

        /// <summary>
        /// Agrega un nuevo reporte de análisis estratégico
        /// al contexto de la aplicación.
        /// </summary>
        public async Task AddAsync(
            ReporteAnalisisEstrategico
                reporteAnalisisEstrategico,
            CancellationToken cancellationToken)
        {
            await _context.ReporteAnalisisEstrategicos.AddAsync(
                reporteAnalisisEstrategico,
                cancellationToken);
        }

        /// <summary>
        /// Marca un reporte de análisis estratégico
        /// para que sus cambios sean actualizados.
        /// </summary>
        public void Update(
            ReporteAnalisisEstrategico
                reporteAnalisisEstrategico)
        {
            _context.ReporteAnalisisEstrategicos.Update(
                reporteAnalisisEstrategico);
        }

        /// <summary>
        /// Marca un reporte de análisis estratégico
        /// para que sea eliminado.
        /// </summary>
        public void Delete(
            ReporteAnalisisEstrategico
                reporteAnalisisEstrategico)
        {
            _context.ReporteAnalisisEstrategicos.Remove(
                reporteAnalisisEstrategico);
        }

        #endregion
    }
}
