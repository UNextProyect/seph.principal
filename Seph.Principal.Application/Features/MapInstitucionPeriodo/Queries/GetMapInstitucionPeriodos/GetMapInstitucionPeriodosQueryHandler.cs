using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodos
{
    /// <summary>
    /// Obtiene todas las asignaciones
    /// de periodos por institución.
    /// </summary>
    public sealed class GetMapInstitucionPeriodosQueryHandler(
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository)
        : IRequestHandler<
            GetMapInstitucionPeriodosQuery,
            ResponseWrapper<IReadOnlyList<MapInstitucionPeriodoDto>>>
    {
        public async Task<
            ResponseWrapper<IReadOnlyList<MapInstitucionPeriodoDto>>> Handle(
            GetMapInstitucionPeriodosQuery request,
            CancellationToken cancellationToken)
        {
            var asignaciones =
                await mapInstitucionPeriodoRepository.GetAllAsync(
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
                "Asignaciones de periodos obtenidas correctamente.");
        }
    }
}
