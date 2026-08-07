using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportesPatenteByPeriodo
{
    /// <summary>
    /// Obtiene las patentes registradas
    /// durante un periodo institucional.
    /// </summary>
    public sealed record GetReportesPatenteByPeriodoQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<
            ResponseWrapper<
                IReadOnlyList<ReportePatenteDto>>>;
}