using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Seph.Principal.Infraestructure.Persistence.Repositories
{
    public sealed class MapInstitucionPeriodoRepository : IMapInstitucionPeriodoRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public MapInstitucionPeriodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        public async Task<IReadOnlyList<MapInstitucionPeriodo>> GetAllAsync(
      CancellationToken cancellationToken)
        {
            return await _context.MapInstitucionPeriodos
                .AsNoTracking()
                .Include(x => x.Institucion)
                .Include(x => x.Periodo)
                    .ThenInclude(x => x.TipoPeriodo)
                .OrderByDescending(x => x.DateTimeFechaRegistro)
                .ToListAsync(cancellationToken);
        }

        /*
         * Obtiene todos los periodos asignados
        * a una institución.
        */
   public async Task<IReadOnlyList<MapInstitucionPeriodo>> GetByInstitucionAsync(
            long idInstitucion,
            CancellationToken cancellationToken)
        {
            return await _context.MapInstitucionPeriodos
                .AsNoTracking()
                .Include(x => x.Institucion)
                .Include(x => x.Periodo)
                    .ThenInclude(x => x.TipoPeriodo)
                .Where(x =>
                    x.IdInstitucion == idInstitucion)
                .OrderByDescending(x => x.Periodo.IntAnio)
                .ThenByDescending(x => x.Periodo.IntNumeroPeriodo)
                .ThenByDescending(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<MapInstitucionPeriodo?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
        {
            return await _context.MapInstitucionPeriodos
                .AsNoTracking()
                .Include(x => x.Institucion)
                .Include(x => x.Periodo)
                    .ThenInclude(x => x.TipoPeriodo)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<MapInstitucionPeriodo?> GetByInstitucionPeriodoAsync(
       long idInstitucion,
       long idPeriodo,
       CancellationToken cancellationToken)
        {
            return await _context.MapInstitucionPeriodos
                .Include(x => x.Institucion)
                .Include(x => x.Periodo)
                    .ThenInclude(x => x.TipoPeriodo)
                .FirstOrDefaultAsync(
                    x =>
                        x.IdInstitucion == idInstitucion &&
                        x.IdPeriodo == idPeriodo,
                    cancellationToken);
        }

        public async Task AddAsync(MapInstitucionPeriodo mapInstitucionPeriodo, CancellationToken cancellationToken)
        {
            await _context.MapInstitucionPeriodos.AddAsync(
                mapInstitucionPeriodo,
                cancellationToken);
        }
        /*
 * Obtiene el periodo activo asignado a una institución.
 * Se considera activo cuando la relación está habilitada
 * y la captura se encuentra abierta.
 */
        public async Task<MapInstitucionPeriodo?> GetPeriodoActivoByInstitucionAsync(
       long idInstitucion,
       CancellationToken cancellationToken)
        {
            return await _context.MapInstitucionPeriodos
                .AsNoTracking()
                .Include(x => x.Institucion)
                .Include(x => x.Periodo)
                    .ThenInclude(x => x.TipoPeriodo)
                .Where(x =>
                    x.IdInstitucion == idInstitucion &&
                    x.BitActivo &&
                    x.BitCapturaAbierta)
                .OrderByDescending(x => x.DateFechaApertura)
                .ThenByDescending(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public void Update(MapInstitucionPeriodo mapInstitucionPeriodo)
        {
            _context.MapInstitucionPeriodos.Update(mapInstitucionPeriodo);
        }

        public void Delete(MapInstitucionPeriodo mapInstitucionPeriodo)
        {
            _context.MapInstitucionPeriodos.Remove(mapInstitucionPeriodo);
        }
       
        #endregion
    }
}
