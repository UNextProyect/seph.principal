using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.GetReporteAnalisisEstrategico
{
    /// <summary>
    /// Obtiene el reporte de análisis estratégico
    /// asociado a una institución y periodo.
    /// </summary>
    public sealed record GetReporteAnalisisEstrategicoQuery(
        long IdMapInstitucionPeriodo)
        : IRequest<
            ResponseWrapper<ReporteAnalisisEstrategicoDto>>;
}
