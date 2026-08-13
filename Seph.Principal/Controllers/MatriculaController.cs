using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.CatPeriodos.Queries.GetCatPeriodo;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.CreateMapInstitucionPeriodo;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetPeriodoActivoInstitucion;
using Seph.Principal.Application.Features.ReporteMatricula.Commands;
using Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatricula;
using Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaComparativo;
using Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaEstadisticas;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class MatriculaController(IMediator mediator) : ControllerBase
    {
        // Obtiene los periodos disponibles para captura.
        [HttpGet("periodos")]
        public async Task<IActionResult> GetPeriodos(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetCatPeriodoQuery(),
                cancellationToken);

            return Ok(response);
        }

        // Asigna un periodo a una institución.
        [HttpPost("institucion-periodo")]
        public async Task<IActionResult> CreateInstitucionPeriodo(
            CreateMapInstitucionPeriodoCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Registra el reporte de matrícula.
        [HttpPost("reporte")]
        public async Task<IActionResult> CreateReporteMatricula(
            CreateReporteMatriculaCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Actualiza el reporte de matrícula de un periodo institucional.
        [HttpPut("reporte")]
        public async Task<IActionResult> UpdateReporteMatricula(
            UpdateReporteMatriculaCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                command,
                cancellationToken);

            return Ok(response);
        }

        // Obtiene el reporte de matrícula registrado para un periodo institucional.
        [HttpGet("reporte/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteMatricula(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteMatriculaQuery(idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
        // Obtiene el comparativo de matrícula
        // entre dos periodos seleccionados.
        [HttpGet(
            "reporte-comparativo/" +
            "{idMapPeriodoBase:long}/" +
            "{idMapPeriodoComparacion:long}")]
        public async Task<IActionResult>
            GetReporteMatriculaComparativo(
                long idMapPeriodoBase,
                long idMapPeriodoComparacion,
                CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteMatriculaComparativoQuery(
                    idMapPeriodoBase,
                    idMapPeriodoComparacion),
                cancellationToken);

            return Ok(response);
        }
        // Obtiene estadísticas listas para dashboard, gráficas o reportes.
        [HttpGet("estadisticas/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult> GetReporteMatriculaEstadisticas(
            long idMapInstitucionPeriodo,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetReporteMatriculaEstadisticasQuery(idMapInstitucionPeriodo),
                cancellationToken);

            return Ok(response);
        }
        // Obtiene el periodo activo asignado a una institución.
        [HttpGet("periodo-activo/{idInstitucion:long}")]
        public async Task<IActionResult> GetPeriodoActivoInstitucion(
            long idInstitucion,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(
                new GetPeriodoActivoInstitucionQuery(idInstitucion),
                cancellationToken);

            return Ok(response);
        }
    }
}
