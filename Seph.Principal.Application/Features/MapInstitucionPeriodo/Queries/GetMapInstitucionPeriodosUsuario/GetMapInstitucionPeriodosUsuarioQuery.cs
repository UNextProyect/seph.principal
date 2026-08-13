using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodosUsuario
{
    /// <summary>
    /// Obtiene los periodos asignados
    /// a la institución del usuario autenticado.
    /// </summary>
    public sealed record GetMapInstitucionPeriodosUsuarioQuery
        : IRequest<
            ResponseWrapper<
                IReadOnlyList<MapInstitucionPeriodoDto>>>;
}
