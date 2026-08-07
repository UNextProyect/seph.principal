using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Queries.GetActiveCatPreguntaAnalisis
{
    /// <summary>
    /// Consulta las preguntas activas disponibles
    /// para la captura del análisis estratégico.
    /// </summary>
    public sealed record GetActiveCatPreguntaAnalisisQuery()
        : IRequest<
            ResponseWrapper<
                IReadOnlyList<CatPreguntaAnalisisDto>>>;
}
