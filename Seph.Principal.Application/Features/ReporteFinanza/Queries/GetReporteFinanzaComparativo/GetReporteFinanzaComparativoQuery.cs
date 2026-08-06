using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;

namespace Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaComparativo
{
    /// <summary>
    /// Solicita el comparativo de los indicadores financieros
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed record GetReporteFinanzaComparativoQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<ReporteFinanzaComparativoDto>>>;
}