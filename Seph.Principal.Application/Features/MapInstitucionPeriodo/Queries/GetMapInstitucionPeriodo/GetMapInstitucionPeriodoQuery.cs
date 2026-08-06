using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodo
{
    /// <summary>
    /// Obtiene una asignación de periodo
    /// por su identificador.
    /// </summary>
    public sealed record GetMapInstitucionPeriodoQuery(
        long Id)
        : IRequest<ResponseWrapper<MapInstitucionPeriodoDto>>;
}
