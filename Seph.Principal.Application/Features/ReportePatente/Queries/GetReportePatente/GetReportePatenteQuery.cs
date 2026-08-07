using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatente
{
    /// <summary>
    /// Obtiene un reporte de patente
    /// mediante su identificador.
    /// </summary>
    public sealed record GetReportePatenteQuery(
        long Id)
        : IRequest<ResponseWrapper<ReportePatenteDto>>;
}