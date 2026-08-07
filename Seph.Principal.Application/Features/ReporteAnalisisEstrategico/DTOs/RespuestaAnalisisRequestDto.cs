using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs
{
    /// <summary>
    /// Representa una respuesta enviada
    /// para registrar o actualizar un análisis estratégico.
    /// </summary>
    public sealed class RespuestaAnalisisRequestDto
    {
        /// <summary>
        /// Identificador de la pregunta
        /// que se está respondiendo.
        /// </summary>
        public long IdPreguntaAnalisis { get; set; }

        /// <summary>
        /// Respuesta capturada por la institución.
        /// Puede permanecer vacía.
        /// </summary>
        public string? StrRespuesta { get; set; }
    }
}
