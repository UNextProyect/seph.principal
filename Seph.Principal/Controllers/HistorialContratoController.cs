using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.HistorialContratos.Commands;
using Seph.Principal.Application.Features.HistorialContratos.DTOs;

namespace Seph.Principal.Controllers
{

    public class HistorialContratoController(ISender sender) : ApiControllerBase
    {
        #region Create

        [AllowAnonymous]
        [HttpPost("create-historial-contrato")]
        public async Task<IActionResult> Create(
            [FromBody] HistorialContratoDto request,
            CancellationToken cancellationToken)
        {
            var response = await sender.Send(
                new CreateHistorialContratoCommand(
                    request.DateFechaIngreso,
                    request.DateFechaInicio,
                    request.IdEmpleado,
                    request.IdInstitucion,
                    request.IdTipoPersonal,
                    request.IdTipoContrato,
                    request.StrOtroTipoContrato,
                    request.IdArea,
                    request.DateTimeFechaRegistro,
                    request.BitActivo,
                    request.DateTimeFechaBaja,
                    request.IdUsuarioRegistro
                ));

            return FromResponse(response);
        }

        #endregion
    }
}