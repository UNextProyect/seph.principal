using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;

namespace Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaComparativo
{
    /// <summary>
    /// Solicita la comparación de los indicadores
    /// financieros entre dos periodos seleccionados.
    /// </summary>
    public sealed record GetReporteFinanzaComparativoQuery(
        long IdMapPeriodoBase,
        long IdMapPeriodoComparacion)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<ReporteFinanzaComparativoDto>>>;
}