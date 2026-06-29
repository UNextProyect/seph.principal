using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado;
using Seph.Principal.Application.Features.Empleados.DTOs;

namespace Seph.Principal.Controllers
{
    public class EmpleadoController(ISender sender) : ApiControllerBase
    {
        #region Create
        [AllowAnonymous]
        [HttpPost("create-empleado")]
        public async Task<IActionResult> Create([FromBody] CreateEmpleadoRequest request, CancellationToken cancellationToken)
        {
            var response = await sender.Send(new CreateEmpleadoCommand(
                request.StrNombre,
                request.StrApellidoPat,
                request.StrApellidoMat,
                request.StrCurp,
                request.IdSexo,
                request.IdInstitucion,
                request.DateTimeFechaRegistro,
                request.IdUsuarioRegistro,
                request.BitActivo,
                request.DateTimeFechaBaja
                ));

            return FromResponse(response);
        }
        #endregion
    }
}
