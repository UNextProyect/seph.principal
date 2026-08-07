using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.CreateReporteAnalisisEstrategico;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.GetReporteAnalisisEstrategico;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.UpdateReporteAnalisisEstrategico;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class ReporteAnalisisEstrategicoController(
        IMediator mediator)
        : ControllerBase
    {
        #region Create

        /// <summary>
        /// Registra el reporte de análisis estratégico
        /// para una institución y periodo.
        /// POST /api/v1/ReporteAnalisisEstrategico/reporte
        /// </summary>
        [HttpPost("reporte")]
        public async Task<IActionResult>
            CreateReporteAnalisisEstrategico(
                CreateReporteAnalisisEstrategicoCommand command,
                CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    command,
                    cancellationToken);

            return Ok(response);
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza las respuestas del reporte
        /// de análisis estratégico existente.
        /// PUT /api/v1/ReporteAnalisisEstrategico/reporte
        /// </summary>
        [HttpPut("reporte")]
        public async Task<IActionResult>
            UpdateReporteAnalisisEstrategico(
                UpdateReporteAnalisisEstrategicoCommand command,
                CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    command,
                    cancellationToken);

            return Ok(response);
        }

        #endregion

        #region Get

        /// <summary>
        /// Obtiene las preguntas y respuestas del análisis
        /// estratégico para una institución y periodo.
        /// GET /api/v1/ReporteAnalisisEstrategico/reporte/{idMapInstitucionPeriodo}
        /// </summary>
        [HttpGet(
            "reporte/{idMapInstitucionPeriodo:long}")]
        public async Task<IActionResult>
            GetReporteAnalisisEstrategico(
                long idMapInstitucionPeriodo,
                CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    new GetReporteAnalisisEstrategicoQuery(
                        idMapInstitucionPeriodo),
                    cancellationToken);

            return Ok(response);
        }

        #endregion
    }
}
