using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodos
{
    /// <summary>
    /// Obtiene todas las asignaciones
    /// de periodos por institución.
    /// </summary>
    public sealed record GetMapInstitucionPeriodosQuery
        : IRequest<
            ResponseWrapper<
                IReadOnlyList<MapInstitucionPeriodoDto>>>;
}
