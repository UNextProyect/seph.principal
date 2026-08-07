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
    public sealed class CatPreguntaAnalisisRepository
        : ICatPreguntaAnalisisRepository
    {
        private readonly ApplicationDbContext _context;

        #region Constructor

        public CatPreguntaAnalisisRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos de la clase

        /*
         * Obtiene todas las preguntas registradas,
         * incluyendo activas e inactivas.
         */
        public async Task<IReadOnlyList<CatPreguntaAnalisis>>
            GetAllAsync(
                CancellationToken cancellationToken)
        {
            return await _context.CatPreguntasAnalisis
                .AsNoTracking()
                .OrderBy(x => x.IntOrden)
                .ThenBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /*
         * Obtiene las preguntas activas
         * en el orden correspondiente.
         */
        public async Task<IReadOnlyList<CatPreguntaAnalisis>>
            GetActiveAsync(
                CancellationToken cancellationToken)
        {
            return await _context.CatPreguntasAnalisis
                .AsNoTracking()
                .Where(x => x.BitActivo)
                .OrderBy(x => x.IntOrden)
                .ThenBy(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        /*
         * Obtiene una pregunta
         * por su identificador.
         */
        public async Task<CatPreguntaAnalisis?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatPreguntasAnalisis
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        /*
         * Verifica si ya existe una pregunta
         * con el mismo texto.
         */
        public async Task<bool> ExistsByPreguntaAsync(
            string strPregunta,
            CancellationToken cancellationToken)
        {
            return await _context.CatPreguntasAnalisis
                .AsNoTracking()
                .AnyAsync(
                    x => x.StrPregunta == strPregunta,
                    cancellationToken);
        }

        /*
         * Verifica si existe otra pregunta
         * con el mismo texto, excluyendo
         * el registro que se está editando.
         */
        public async Task<bool> ExistsByPreguntaExceptIdAsync(
            string strPregunta,
            long id,
            CancellationToken cancellationToken)
        {
            return await _context.CatPreguntasAnalisis
                .AsNoTracking()
                .AnyAsync(
                    x => x.StrPregunta == strPregunta
                        && x.Id != id,
                    cancellationToken);
        }

        /*
         * Obtiene el siguiente número de orden
         * disponible para una pregunta nueva.
         */
        public async Task<int> GetNextOrdenAsync(
            CancellationToken cancellationToken)
        {
            var ultimoOrden = await _context
                .CatPreguntasAnalisis
                .AsNoTracking()
                .Select(x => (int?)x.IntOrden)
                .MaxAsync(cancellationToken);

            return (ultimoOrden ?? 0) + 1;
        }

        /*
         * Agrega una nueva pregunta
         * al catálogo.
         */
        public async Task AddAsync(
            CatPreguntaAnalisis catPreguntaAnalisis,
            CancellationToken cancellationToken)
        {
            await _context.CatPreguntasAnalisis.AddAsync(
                catPreguntaAnalisis,
                cancellationToken);
        }

        /*
         * Marca una pregunta
         * para actualización.
         */
        public void Update(
            CatPreguntaAnalisis catPreguntaAnalisis)
        {
            _context.CatPreguntasAnalisis.Update(
                catPreguntaAnalisis);
        }

        #endregion
    }
}
