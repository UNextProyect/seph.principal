using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteComparativo
{
    /// <summary>
    /// Solicita la comparación del total de patentes
    /// entre dos periodos seleccionados.
    /// </summary>
    public sealed record GetReportePatenteComparativoQuery(
        long IdMapPeriodoBase,
        long IdMapPeriodoComparacion)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReportePatenteComparativoDto>>>;
}