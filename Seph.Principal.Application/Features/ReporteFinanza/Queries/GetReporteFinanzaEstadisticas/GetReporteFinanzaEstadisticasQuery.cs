using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;

namespace Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaEstadisticas
{
    /// <summary>
    /// Solicita las estadísticas financieras
    /// de una institución durante un periodo.
    /// </summary>
    public sealed record GetReporteFinanzaEstadisticasQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReporteFinanzaEstadisticasDto>>;
}