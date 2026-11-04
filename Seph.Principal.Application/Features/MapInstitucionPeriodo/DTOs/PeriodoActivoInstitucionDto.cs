using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs
{
    /*
   * Información del periodo activo asignado
   * a una institución.
   */
    public sealed record PeriodoActivoInstitucionDto(
        long IdMapInstitucionPeriodo,
        long IdInstitucion,
        long IdPeriodo,
        string StrPeriodo,
        string StrDescripcionPeriodo,
        bool BitCapturaAbierta,
        DateTime? DateFechaApertura,
        DateTime? DateFechaCierre);
}
