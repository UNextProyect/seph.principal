using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.ReporteFinanza.Commands;
using Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanza;
using Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaComparativo;
using Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaEstadisticas;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class FinanzaController(IMediator mediator)
        : ControllerBase
    {
        // Registra el reporte financiero.
        [HttpPost("reporte")]
        public async Task<IActionResult> CreateReporteFinanza(
            CreateReporteFinanzaCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Actualiza el reporte financiero de un periodo institucional.
        [HttpPut("reporte")]
        public async Task<IActionResult> UpdateReporteFinanza(
            UpdateReporteFinanzaCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el reporte financiero registrado
        // para un periodo institucional.
        [HttpGet("reporte/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteFinanza(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteFinanzaQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Compara los reportes financieros de dos periodos seleccionados.
        [HttpGet(
            "reporte-comparativo/{idMapPeriodoBase:long}/" +
            "{idMapPeriodoComparacion:long}")]
        public async Task<IActionResult>
            GetReporteFinanzaComparativo(
                long idMapPeriodoBase,
                long idMapPeriodoComparacion,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteFinanzaComparativoQuery(
                    idMapPeriodoBase,
                    idMapPeriodoComparacion),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene estadísticas financieras listas
        // para dashboard, gráficas o reportes.
        [HttpGet("estadisticas/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult>
            GetReporteFinanzaEstadisticas(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteFinanzaEstadisticasQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
    }
}