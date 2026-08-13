using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.ReportePatente.Commands;
using Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatente;
using Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteComparativo;
using Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteEstadisticas;
using Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportesPatenteByPeriodo;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class PatenteController(IMediator mediator)
        : ControllerBase
    {
        // Registra un nuevo reporte de patente.
        [HttpPost("reporte")]
        public async Task<IActionResult> CreateReportePatente(
            CreateReportePatenteCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Actualiza un reporte de patente.
        [HttpPut("reporte")]
        public async Task<IActionResult> UpdateReportePatente(
            UpdateReportePatenteCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Obtiene una patente mediante su identificador.
        [HttpGet("reporte/{id:long}")]
        public async Task<IActionResult> GetReportePatente(
            long id,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportePatenteQuery(id),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene las patentes registradas
        // durante un periodo institucional.
        [HttpGet(
            "reportes-periodo/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult>
            GetReportesPatenteByPeriodo(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportesPatenteByPeriodoQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }

        // Compara el total de patentes
        // registrado en dos periodos seleccionados.
        [HttpGet(
            "reporte-comparativo/{idMapPeriodoBase:long}/" +
            "{idMapPeriodoComparacion:long}")]
        public async Task<IActionResult>
            GetReportePatenteComparativo(
                long idMapPeriodoBase,
                long idMapPeriodoComparacion,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportePatenteComparativoQuery(
                    idMapPeriodoBase,
                    idMapPeriodoComparacion),
                cancellationToken);

            return Ok(response);
        }

        // Obtiene estadísticas listas para dashboard,
        // gráficas o reportes.
        [HttpGet(
            "estadisticas/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult>
            GetReportePatenteEstadisticas(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReportePatenteEstadisticasQuery(
                    idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
    }
}