using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Queries.GetCatPreguntaAnalisis
{
    /// <summary>
    /// Consulta todas las preguntas registradas
    /// para el análisis estratégico.
    /// </summary>
    public sealed record GetCatPreguntaAnalisisQuery()
        : IRequest<
            ResponseWrapper<
                IReadOnlyList<CatPreguntaAnalisisDto>>>;
}
