using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs
{
    /*
   * Representa una pregunta disponible
   * para el análisis estratégico.
   */
    public sealed record CatPreguntaAnalisisDto(
        long Id,
        string StrPregunta,
        DateTime DateTimeFechaRegistro,
        bool BitActivo,
        int IntOrden);
}
