using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;

namespace Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionComparativo
{
    /// <summary>
    /// Solicita la comparación de los indicadores
    /// de vinculación entre dos periodos seleccionados.
    /// </summary>
    public sealed record GetReporteVinculacionComparativoQuery(
        long IdMapPeriodoBase,
        long IdMapPeriodoComparacion)
        : IRequest<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteVinculacionComparativoDto>>>;
}