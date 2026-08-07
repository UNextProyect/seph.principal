using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    /*
    * Representa el reporte de análisis estratégico
    * registrado por una institución durante un periodo.
    */
    public sealed class ReporteAnalisisEstrategico
    {
        public long Id { get; set; }

        /*
         * Relación entre la institución
         * y el periodo de captura.
         */
        public long IdMapInstitucionPeriodo { get; set; }

        /*
         * Fecha en la que se registró
         * el análisis estratégico.
         */
        public DateTime DateTimeFechaRegistro { get; set; }

        /*
         * Usuario que realizó
         * el registro del reporte.
         */
        public Guid IdUsuarioRegistro { get; set; }

        /*
         * Indica si el reporte
         * se encuentra activo.
         */
        public bool BitActivo { get; set; }
     

        /*
         * Respuestas registradas
         * dentro del análisis estratégico.
         */
        public ICollection<RespuestaAnalisis> RespuestasAnalisis
        {
            get;
            set;
        } = new List<RespuestaAnalisis>();
    }
}
