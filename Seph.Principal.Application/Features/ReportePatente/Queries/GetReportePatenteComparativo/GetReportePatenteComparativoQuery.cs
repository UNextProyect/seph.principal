using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteComparativo
{
    /// <summary>
    /// Solicita el comparativo del total de patentes
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed record GetReportePatenteComparativoQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<ReportePatenteComparativoDto>>>;
}