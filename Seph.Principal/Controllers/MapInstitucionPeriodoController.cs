using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.CreateMapInstitucionPeriodo;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.UpdateMapInstitucionPeriodo;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodo;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodos;

namespace Seph.Principal.Controllers
{
    /// <summary>
    /// Administra la asignación de periodos
    /// a las instituciones.
    /// </summary>
    public sealed class MapInstitucionPeriodoController(ISender sender)
        : ApiControllerBase
    {
        #region Create

        /// <summary>
        /// Asigna un periodo a una institución.
        /// POST /api/v1/mapinstitucionperiodo
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateMapInstitucionPeriodoCommand command,
            CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                command,
                cancellationToken);

            return FromResponse(response);
        }

        #endregion

        #region Update

        /// <summary>
        /// Actualiza una asignación de periodo institucional.
        /// PUT /api/v1/mapinstitucionperiodo/{id}
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateMapInstitucionPeriodoCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(
                    "El identificador de la ruta no coincide con el cuerpo de la solicitud.");
            }

            var response = await sender.Send(
                command,
                cancellationToken);

            return FromResponse(response);
        }

        #endregion

        #region Get

        /// <summary>
        /// Obtiene todas las asignaciones
        /// de periodos por institución.
        /// GET /api/v1/mapinstitucionperiodo
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                new GetMapInstitucionPeriodosQuery(),
                cancellationToken);

            return FromResponse(response);
        }

        /// <summary>
        /// Obtiene una asignación por su identificador.
        /// GET /api/v1/mapinstitucionperiodo/{id}
        /// </summary>
        [Authorize]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(
            long id,
            CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                new GetMapInstitucionPeriodoQuery(id),
                cancellationToken);

            return FromResponse(response);
        }

        #endregion
    }
}
