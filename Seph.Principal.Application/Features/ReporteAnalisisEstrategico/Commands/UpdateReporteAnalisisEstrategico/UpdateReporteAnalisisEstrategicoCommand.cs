using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.UpdateReporteAnalisisEstrategico
{
    /// <summary>
    /// Comando para actualizar un reporte
    /// de análisis estratégico existente.
    /// </summary>
    public sealed record UpdateReporteAnalisisEstrategicoCommand(
        long IdMapInstitucionPeriodo,
        List<RespuestaAnalisisRequestDto> RespuestasAnalisis)
        : IRequest<
            ResponseWrapper<ReporteAnalisisEstrategicoDto>>;
}
