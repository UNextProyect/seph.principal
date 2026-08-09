using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado;
using Seph.Principal.Application.Features.Empleados.Commands.UpdateDatosAcademicos;
using Seph.Principal.Application.Features.Empleados.Commands.UpdateEmpleadoBasico;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Application.Features.Empleados.Queries.GetEmpleadoById;
using Seph.Principal.Application.Features.Empleados.Queries.GetRegistrosPersonal;
using System.Net;

namespace Seph.Principal.Controllers
{
    public class EmpleadoController(ISender sender, ICurrentUserService currentUserService) : ApiControllerBase
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
                request.StrRutaIne,
                request.StrRutaFotografia,
                request.DateTimeFechaRegistro,
                request.IdUsuarioRegistro,
                request.BitActivo,
                request.DateTimeFechaBaja
                ));

            return FromResponse(response);
        }

        /// <summary>
        /// Actualiza el SNI/SNII y los perfiles académicos (uno o varios) de un
        /// empleado ya registrado. Paso 2 del formulario de Información Personal.
        /// PUT /api/v1/empleado/{id}/datos-academicos
        /// </summary>
        [AllowAnonymous]
        [HttpPut("{id:long}/datos-academicos")]
        public async Task<IActionResult> UpdateDatosAcademicos(
            long id,
            [FromBody] UpdateDatosAcademicosRequest request,
            CancellationToken cancellationToken)
        {
            var response = await sender.Send(new UpdateDatosAcademicosEmpleadoCommand(
                id,
                request.StrSNII,
                request.IdsPerfilAcademico
                ));

            return FromResponse(response);
        }

        /// <summary>
        /// Actualiza los datos básicos (Nombre, apellidos, CURP, sexo) de un
        /// empleado ya registrado. Se usa al retomar un registro incompleto.
        /// PUT /api/v1/empleado/{id}
        /// </summary>
        [AllowAnonymous]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateBasico(
            long id,
            [FromBody] UpdateEmpleadoBasicoRequest request,
            CancellationToken cancellationToken)
        {
            var response = await sender.Send(new UpdateEmpleadoBasicoCommand(
                id,
                request.StrNombre,
                request.StrApellidoPat,
                request.StrApellidoMat,
                request.StrCurp,
                request.IdSexo,
                request.StrRutaIne,
                request.StrRutaFotografia),
              cancellationToken);
                

            return FromResponse(response);
        }
        #endregion

        #region Get

        /// <summary>
        /// Detalle completo de un empleado (datos básicos + académicos),
        /// usado para poblar el formulario al retomar un registro incompleto.
        /// GET /api/v1/empleado/{id}
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            return FromResponse(await sender.Send(new GetEmpleadoByIdQuery(id), cancellationToken));
        }
        /// <summary>
        /// Concentrado de registros de personal capturados por el usuario autenticado
        /// (empleado + historial de contrato + catálogos resueltos).
        /// GET /api/v1/empleado/get-registros
        /// </summary>
        [Authorize]
        [HttpGet("get-registros")]
        public async Task<IActionResult> GetRegistros(CancellationToken cancellationToken)
        {
            if (currentUserService.UserId is not { } userId)
            {
                return FromResponse(ResponseFactory.Failure<IReadOnlyList<RegistroPersonalDto>>(
                    "Usuario no autenticado", HttpStatusCode.Unauthorized));
            }

            return FromResponse(await sender.Send(
                new GetRegistrosPersonalQuery(userId), cancellationToken));
        }
        #endregion
    }
}
