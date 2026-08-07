using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteEstadisticas
{
    /// <summary>
    /// Solicita las estadísticas de patentes
    /// de una institución durante un periodo.
    /// </summary>
    public sealed record GetReportePatenteEstadisticasQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReportePatenteEstadisticasDto>>;
}