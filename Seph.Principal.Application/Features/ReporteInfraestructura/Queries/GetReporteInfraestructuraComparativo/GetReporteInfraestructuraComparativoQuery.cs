using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;

namespace Seph.Principal.Application.Features.ReporteInfraestructura
    .Queries.GetReporteInfraestructuraComparativo
{
    /// <summary>
    /// Solicita la comparación de los indicadores
    /// de infraestructura entre dos periodos seleccionados.
    /// </summary>
    public sealed record GetReporteInfraestructuraComparativoQuery(
        long IdMapPeriodoBase,
        long IdMapPeriodoComparacion)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteInfraestructuraComparativoDto>>>;
}