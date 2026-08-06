using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs
{
    /// <summary>
    /// Representa la asignación de un periodo
    /// a una institución.
    /// </summary>
    public sealed class MapInstitucionPeriodoDto
    {
        public long Id { get; set; }

        public long IdInstitucion { get; set; }

        public string StrInstitucion { get; set; } = string.Empty;

        public string? StrSiglasInstitucion { get; set; }

        public long IdPeriodo { get; set; }

        public string StrPeriodo { get; set; } = string.Empty;

        public string StrDescripcionPeriodo { get; set; } = string.Empty;

        public int IntAnio { get; set; }

        public int IntNumeroPeriodo { get; set; }

        public DateTime DateFechaInicioPeriodo { get; set; }

        public DateTime DateFechaFinPeriodo { get; set; }

        public long IdTipoPeriodo { get; set; }

        public string StrTipoPeriodo { get; set; } = string.Empty;

        public bool BitCapturaAbierta { get; set; }

        public DateTime? DateFechaApertura { get; set; }

        public DateTime? DateFechaCierre { get; set; }

        public DateTime DateTimeFechaRegistro { get; set; }

        public Guid IdUsuarioRegistro { get; set; }

        public bool BitActivo { get; set; }
    }
}
