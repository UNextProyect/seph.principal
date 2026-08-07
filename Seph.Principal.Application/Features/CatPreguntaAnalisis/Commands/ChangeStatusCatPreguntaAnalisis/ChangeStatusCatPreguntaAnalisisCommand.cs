using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.ChangeStatusCatPreguntaAnalisis
{
    /*
      * Cambia el estado activo o inactivo
      * de una pregunta de análisis estratégico.
      */
    public sealed record ChangeStatusCatPreguntaAnalisisCommand(
        long Id,
        bool BitActivo)
        : IRequest<ResponseWrapper<CatPreguntaAnalisisDto>>;
}
