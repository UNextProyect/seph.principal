using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.ChangeStatusCatPreguntaAnalisis;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.CreateCatPreguntaAnalisis;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.UpdateCatPreguntaAnalisis;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.Queries.GetActiveCatPreguntaAnalisis;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.Queries.GetCatPreguntaAnalisis;

namespace Seph.Principal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class CatPreguntaAnalisisController(
           IMediator mediator)
           : ControllerBase
    {
        #region Create

        /// <summary>
        /// Registra una nueva pregunta
        /// para el análisis estratégico.
        /// POST /api/v1/CatPreguntaAnalisis/create-pregunta
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-pregunta")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCatPreguntaAnalisisRequest request,
            CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    new CreateCatPreguntaAnalisisCommand(
                        request.StrPregunta),
                    cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza el texto de una pregunta existente.
        /// PUT /api/v1/CatPreguntaAnalisis/{id}
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateCatPreguntaAnalisisRequest request,
            CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    new UpdateCatPreguntaAnalisisCommand(
                        id,
                        request.StrPregunta),
                    cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion

        #region ChangeStatus

        /// <summary>
        /// Activa o desactiva una pregunta existente.
        /// PATCH /api/v1/CatPreguntaAnalisis/{id}/status
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPatch("{id:long}/status")]
        public async Task<IActionResult> ChangeStatus(
            long id,
            [FromBody]
            ChangeStatusCatPreguntaAnalisisRequest request,
            CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    new ChangeStatusCatPreguntaAnalisisCommand(
                        id,
                        request.BitActivo),
                    cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion

        #region Get

        /// <summary>
        /// Obtiene todas las preguntas registradas,
        /// incluyendo activas e inactivas.
        /// GET /api/v1/CatPreguntaAnalisis
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetCatPreguntaAnalisis(
            CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    new GetCatPreguntaAnalisisQuery(),
                    cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        /// <summary>
        /// Obtiene únicamente las preguntas activas
        /// disponibles para la captura institucional.
        /// GET /api/v1/CatPreguntaAnalisis/activas
        /// </summary>
        [Authorize]
        [HttpGet("activas")]
        public async Task<IActionResult>
            GetActiveCatPreguntaAnalisis(
                CancellationToken cancellationToken)
        {
            var response =
                await mediator.Send(
                    new GetActiveCatPreguntaAnalisisQuery(),
                    cancellationToken);

            return StatusCode(
                (int)response.StatusCode,
                response);
        }

        #endregion
    }
}
