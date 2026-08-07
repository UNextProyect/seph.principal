using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    /*
    * Representa una pregunta abierta
    * disponible para el análisis estratégico.
    */
    public sealed class CatPreguntaAnalisis
    {
        public long Id { get; set; }

        /*
         * Texto de la pregunta que se mostrará
         * a todas las instituciones.
         */
        public string StrPregunta { get; set; } =
            string.Empty;

        /*
         * Fecha en la que la pregunta
         * fue registrada.
         */
        public DateTime DateTimeFechaRegistro { get; set; }

        /*
         * Indica si la pregunta se encuentra
         * disponible para nuevas capturas.
         */
        public bool BitActivo { get; set; }

        /*
         * Posición administrativa utilizada
         * para ordenar las preguntas.
         */
        public int IntOrden { get; set; }
    }
}
