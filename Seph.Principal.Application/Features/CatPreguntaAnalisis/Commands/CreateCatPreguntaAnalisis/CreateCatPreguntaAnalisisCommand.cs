using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.CreateCatPreguntaAnalisis
{
    /// <summary>
    /// Comando para registrar una nueva
    /// pregunta de análisis estratégico.
    /// </summary>
    public sealed record CreateCatPreguntaAnalisisCommand(
        string StrPregunta)
        : IRequest<ResponseWrapper<CatPreguntaAnalisisDto>>;
}
