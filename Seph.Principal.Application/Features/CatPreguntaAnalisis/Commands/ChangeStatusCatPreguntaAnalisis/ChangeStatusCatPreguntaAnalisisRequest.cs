using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.ChangeStatusCatPreguntaAnalisis
{
    /// <summary>
    /// Contiene el estado que se asignará
    /// a una pregunta de análisis estratégico.
    /// </summary>
    public sealed record ChangeStatusCatPreguntaAnalisisRequest(
        bool BitActivo);
}
