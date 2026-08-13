using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;

namespace Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaComparativo
{

    /// <summary>
    /// Solicita la comparación de matrícula
    /// entre dos periodos seleccionados.
    /// </summary>
    public sealed record GetReporteMatriculaComparativoQuery(
        long IdMapPeriodoBase,
        long IdMapPeriodoComparacion)
        : IRequest<
            ResponseWrapper<ReporteMatriculaComparativoDto>>;

}
