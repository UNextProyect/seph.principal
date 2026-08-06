using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;

namespace Seph.Principal.Application.Features.ReporteFinanza.Queries.GetReporteFinanza
{
    /// <summary>
    /// Obtiene el reporte financiero
    /// asociado a una institución y periodo.
    /// </summary>
    public sealed record GetReporteFinanzaQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<ResponseWrapper<ReporteFinanzaDto>>;
}