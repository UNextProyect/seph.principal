using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.CreateReporteAnalisisEstrategico
{
    /// <summary>
    /// Comando para registrar un reporte
    /// de análisis estratégico.
    /// </summary>
    public sealed record CreateReporteAnalisisEstrategicoCommand(
        long IdMapInstitucionPeriodo,
        Guid IdUsuarioRegistro,
        List<RespuestaAnalisisRequestDto> RespuestasAnalisis)
        : IRequest<
            ResponseWrapper<ReporteAnalisisEstrategicoDto>>;
}
