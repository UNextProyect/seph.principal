using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.CreateCatPreguntaAnalisis
{
    /// <summary>
    /// Contiene los datos necesarios para registrar
    /// una pregunta de análisis estratégico.
    /// </summary>
    public sealed record CreateCatPreguntaAnalisisRequest(
        string StrPregunta);
}
