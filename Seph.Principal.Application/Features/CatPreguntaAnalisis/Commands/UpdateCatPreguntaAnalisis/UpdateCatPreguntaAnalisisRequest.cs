using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.UpdateCatPreguntaAnalisis
{
    /// <summary>
    /// Contiene los datos necesarios para actualizar
    /// una pregunta de análisis estratégico.
    /// </summary>
    public sealed record UpdateCatPreguntaAnalisisRequest(
        string StrPregunta);
}
