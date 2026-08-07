using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    /*
    * Representa la respuesta registrada
    * para una pregunta del análisis estratégico.
    */
    public sealed class RespuestaAnalisis
    {
        public long Id { get; set; }

        /*
         * Identificador del reporte de análisis
         * estratégico al que pertenece la respuesta.
         */
        public long IdAnalisisEstrategico { get; set; }

        /*
         * Identificador de la pregunta
         * relacionada con la respuesta.
         */
        public long IdPreguntaAnalisis { get; set; }

        /*
         * Fecha en la que se registró
         * la respuesta.
         */
        public DateTime DateTimeFechaRegistro { get; set; }

        /*
         * Respuesta escrita por la institución.
         *
         * Puede ser null porque las preguntas
         * no serán obligatorias por el momento.
         */
        public string? StrRespuesta { get; set; }

        /*
         * Copia histórica de la pregunta
         * al momento de registrar la respuesta.
         */
        public string StrPregunta { get; set; } =
            string.Empty;

        /*
         * Reporte de análisis estratégico
         * al que pertenece la respuesta.
         */
        public ReporteAnalisisEstrategico
            AnalisisEstrategico
        { get; set; } =
                null!;

        /*
         * Pregunta original relacionada
         * con la respuesta.
         */
        public CatPreguntaAnalisis
            PreguntaAnalisis
        { get; set; } =
                null!;
    }
}
