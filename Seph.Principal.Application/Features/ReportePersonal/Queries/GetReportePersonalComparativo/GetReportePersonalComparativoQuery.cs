using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalComparativo
{
    /*
     * Solicita la comparación de los reportes de personal
     * correspondientes a dos periodos seleccionados.
     */
    public sealed record GetReportePersonalComparativoQuery(
        long IdMapPeriodoBase,
        long IdMapPeriodoComparacion)
        : IRequest<
            ResponseWrapper<ReportePersonalComparativoDto>>;
}