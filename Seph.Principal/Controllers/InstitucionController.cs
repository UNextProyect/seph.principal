using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion;
using Seph.Principal.Application.Features.Instituciones.DTOs;
using Seph.Principal.Application.Features.Instituciones.Queries.GetInstituciones;

namespace Seph.Principal.Controllers
{
    public class InstitucionController(ISender sender) : ApiControllerBase
    {
        #region Create
        /// <summary>
        /// Solo el SuperAdmin crea instituciones.
        /// POST /api/v1/institucion/create-institucion
        /// </summary>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-institucion")]
        public async Task<IActionResult> Create([FromBody] CreateInstitucionRequest request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                new CreateInstitucionCommand(
                    request.StrNombre,
                    request.StrSiglas,
                    request.StrCct,
                    request.StrDireccion,
                    request.DateFechaCreacion,
                    request.StrDecretoCreacion,
                    request.StrSitioWeb,
                    request.StrCorreoInstitucional,
                    request.StrTelefonoInstitucional,
                    request.IdMunicipio),
                cancellationToken);

            return FromResponse(response);
        }
        #endregion

        #region Get
        /// <summary>
        /// Lista todas las instituciones (para asignar Admins, llenar selects, etc.).
        /// GET /api/v1/institucion/get-instituciones
        /// </summary>
        [Authorize]
        [HttpGet("get-instituciones")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetInstitucionesQuery(), cancellationToken);

            return FromResponse(response);
        }
        #endregion
    }
}
