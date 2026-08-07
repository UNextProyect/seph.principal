using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs
{
    /// <summary>
    /// Representa la información de un
    /// reporte de análisis estratégico.
    /// </summary>
    public sealed class ReporteAnalisisEstrategicoDto
    {
        /// <summary>
        /// Identificador del reporte.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador del mapa
        /// institución-periodo.
        /// </summary>
        public long IdMapInstitucionPeriodo { get; set; }

        /// <summary>
        /// Respuestas registradas dentro
        /// del análisis estratégico.
        /// </summary>
        public List<RespuestaAnalisisDto> RespuestasAnalisis
        {
            get;
            set;
        } = new();
    }
}
