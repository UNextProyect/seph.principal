using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /*
     * Define las operaciones de acceso a datos
     * para el catálogo de preguntas de análisis.
     */
    public interface ICatPreguntaAnalisisRepository
    {
        /*
         * Obtiene todas las preguntas registradas,
         * incluyendo activas e inactivas.
         */
        Task<IReadOnlyList<CatPreguntaAnalisis>> GetAllAsync(
            CancellationToken cancellationToken);

        /*
         * Obtiene únicamente las preguntas activas
         * que se mostrarán a las instituciones.
         */
        Task<IReadOnlyList<CatPreguntaAnalisis>> GetActiveAsync(
            CancellationToken cancellationToken);

        /*
         * Obtiene una pregunta por su identificador.
         */
        Task<CatPreguntaAnalisis?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        /*
         * Verifica si ya existe una pregunta
         * con el mismo texto.
         */
        Task<bool> ExistsByPreguntaAsync(
            string strPregunta,
            CancellationToken cancellationToken);

        /*
         * Verifica si existe otra pregunta
         * con el mismo texto, excluyendo
         * el registro que se está editando.
         */
        Task<bool> ExistsByPreguntaExceptIdAsync(
            string strPregunta,
            long id,
            CancellationToken cancellationToken);

        /*
         * Obtiene el siguiente número de orden
         * disponible para una pregunta nueva.
         */
        Task<int> GetNextOrdenAsync(
            CancellationToken cancellationToken);

        /*
         * Agrega una nueva pregunta
         * al catálogo.
         */
        Task AddAsync(
            CatPreguntaAnalisis catPreguntaAnalisis,
            CancellationToken cancellationToken);

        /*
         * Marca una pregunta para actualización.
         *
         * También se utilizará para editar,
         * desactivar y reactivar preguntas.
         */
        void Update(
            CatPreguntaAnalisis catPreguntaAnalisis);
    }
}
