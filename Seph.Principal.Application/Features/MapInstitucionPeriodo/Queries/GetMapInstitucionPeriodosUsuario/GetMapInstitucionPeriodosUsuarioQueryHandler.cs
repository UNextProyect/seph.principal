using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodosUsuario
{
    /// <summary>
    /// Obtiene los periodos correspondientes
    /// a la institución del usuario autenticado.
    /// </summary>
    public sealed class GetMapInstitucionPeriodosUsuarioQueryHandler(
        IMapInstitucionPeriodoRepository
            mapInstitucionPeriodoRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            GetMapInstitucionPeriodosUsuarioQuery,
            ResponseWrapper<
                IReadOnlyList<MapInstitucionPeriodoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyList<MapInstitucionPeriodoDto>>> Handle(
            GetMapInstitucionPeriodosUsuarioQuery request,
            CancellationToken cancellationToken)
        {
            var idInstitucion =
                currentUserService.IdInstitucion;

            if (!idInstitucion.HasValue)
            {
                return ResponseFactory.Failure<
                    IReadOnlyList<MapInstitucionPeriodoDto>>(
                    "El usuario autenticado no tiene una institución asignada.",
                    HttpStatusCode.BadRequest);
            }

            var asignaciones =
                await mapInstitucionPeriodoRepository
                    .GetByInstitucionAsync(
                        idInstitucion.Value,
                        cancellationToken);

            var datos = asignaciones
                .Select(asignacion =>
                    new MapInstitucionPeriodoDto
                    {
                        Id = asignacion.Id,

                        IdInstitucion =
                            asignacion.IdInstitucion,

                        StrInstitucion =
                            asignacion.Institucion.StrNombre,

                        StrSiglasInstitucion =
                            asignacion.Institucion.StrSiglas,

                        IdPeriodo =
                            asignacion.IdPeriodo,

                        StrPeriodo =
                            asignacion.Periodo.StrValor,

                        StrDescripcionPeriodo =
                            asignacion.Periodo.StrDescripcion,

                        IntAnio =
                            asignacion.Periodo.IntAnio,

                        IntNumeroPeriodo =
                            asignacion.Periodo.IntNumeroPeriodo,

                        DateFechaInicioPeriodo =
                            asignacion.Periodo.DateFechaInicio,

                        DateFechaFinPeriodo =
                            asignacion.Periodo.DateFechaFin,

                        IdTipoPeriodo =
                            asignacion.Periodo.IdTipoPeriodo,

                        StrTipoPeriodo =
                            asignacion.Periodo.TipoPeriodo.StrValor,

                        BitCapturaAbierta =
                            asignacion.BitCapturaAbierta,

                        DateFechaApertura =
                            asignacion.DateFechaApertura,

                        DateFechaCierre =
                            asignacion.DateFechaCierre,

                        DateTimeFechaRegistro =
                            asignacion.DateTimeFechaRegistro,

                        IdUsuarioRegistro =
                            asignacion.IdUsuarioRegistro,

                        BitActivo =
                            asignacion.BitActivo
                    })
                .ToList();

            return ResponseFactory.Success<
                IReadOnlyList<MapInstitucionPeriodoDto>>(
                datos,
                "Periodos de la institución obtenidos correctamente.");
        }
    }
}
