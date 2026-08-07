using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.UpdateCatPreguntaAnalisis
{
    /*
     * Actualiza el texto de una pregunta
     * del catálogo de análisis estratégico.
     */
    public sealed record UpdateCatPreguntaAnalisisCommand(
        long Id,
        string StrPregunta)
        : IRequest<ResponseWrapper<CatPreguntaAnalisisDto>>;
}
