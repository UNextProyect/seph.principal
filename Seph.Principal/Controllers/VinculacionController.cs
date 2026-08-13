using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.ReporteVinculacion.Commands;
using Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacion;
using Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionComparativo;
using Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionEstadisticas;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class VinculacionController(IMediator mediator)
        : ControllerBase
    {
        // Registra el reporte de vinculación.
        [HttpPost("reporte")]
        public async Task<IActionResult> CreateReporteVinculacion(
            CreateReporteVinculacionCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Actualiza el reporte de vinculación de un periodo institucional.
        [HttpPut("reporte")]
        public async Task<IActionResult> UpdateReporteVinculacion(
            UpdateReporteVinculacionCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el reporte de vinculación registrado
        // para un periodo institucional.
        [HttpGet("reporte/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteVinculacion(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteVinculacionQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Compara los reportes de vinculación
        // de dos periodos seleccionados.
        [HttpGet(
            "reporte-comparativo/{idMapPeriodoBase:long}/" +
            "{idMapPeriodoComparacion:long}")]
        public async Task<IActionResult>
            GetReporteVinculacionComparativo(
                long idMapPeriodoBase,
                long idMapPeriodoComparacion,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteVinculacionComparativoQuery(
                    idMapPeriodoBase,
                    idMapPeriodoComparacion),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene estadísticas listas para dashboard,
        // gráficas o reportes.
        [HttpGet("estadisticas/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult>
            GetReporteVinculacionEstadisticas(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteVinculacionEstadisticasQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
    }
}