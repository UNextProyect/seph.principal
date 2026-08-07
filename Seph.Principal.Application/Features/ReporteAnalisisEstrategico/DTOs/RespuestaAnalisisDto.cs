using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs
{
    /// <summary>
    /// Representa una respuesta registrada
    /// dentro de un análisis estratégico.
    /// </summary>
    public sealed class RespuestaAnalisisDto
    {
        /// <summary>
        /// Identificador de la respuesta.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador de la pregunta respondida.
        /// </summary>
        public long IdPreguntaAnalisis { get; set; }

        /// <summary>
        /// Texto histórico de la pregunta
        /// al momento de registrar la respuesta.
        /// </summary>
        public string StrPregunta { get; set; } =
            string.Empty;

        /// <summary>
        /// Respuesta capturada por la institución.
        /// Puede permanecer vacía.
        /// </summary>
        public string? StrRespuesta { get; set; }
    }
}
