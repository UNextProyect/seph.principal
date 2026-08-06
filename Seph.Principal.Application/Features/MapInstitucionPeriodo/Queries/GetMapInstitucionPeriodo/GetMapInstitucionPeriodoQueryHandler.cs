using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetMapInstitucionPeriodo
{
    /// <summary>
    /// Obtiene una asignación de periodo
    /// por su identificador.
    /// </summary>
    public sealed class GetMapInstitucionPeriodoQueryHandler(
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository)
        : IRequestHandler<
            GetMapInstitucionPeriodoQuery,
            ResponseWrapper<MapInstitucionPeriodoDto>>
    {
        public async Task<ResponseWrapper<MapInstitucionPeriodoDto>> Handle(
            GetMapInstitucionPeriodoQuery request,
            CancellationToken cancellationToken)
        {
            var asignacion =
                await mapInstitucionPeriodoRepository.GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (asignacion is null)
            {
                return ResponseFactory.Failure<MapInstitucionPeriodoDto>(
                    "No se encontró la asignación del periodo.",
                    HttpStatusCode.NotFound);
            }

            var dto = new MapInstitucionPeriodoDto
            {
                Id = asignacion.Id,

                IdInstitucion = asignacion.IdInstitucion,

                StrInstitucion = asignacion.Institucion.StrNombre,

                StrSiglasInstitucion = asignacion.Institucion.StrSiglas,

                IdPeriodo = asignacion.IdPeriodo,

                StrPeriodo = asignacion.Periodo.StrValor,

                StrDescripcionPeriodo = asignacion.Periodo.StrDescripcion,

                IntAnio = asignacion.Periodo.IntAnio,

                IntNumeroPeriodo = asignacion.Periodo.IntNumeroPeriodo,

                DateFechaInicioPeriodo = asignacion.Periodo.DateFechaInicio,

                DateFechaFinPeriodo = asignacion.Periodo.DateFechaFin,

                IdTipoPeriodo = asignacion.Periodo.IdTipoPeriodo,

                StrTipoPeriodo = asignacion.Periodo.TipoPeriodo.StrValor,

                BitCapturaAbierta = asignacion.BitCapturaAbierta,

                DateFechaApertura = asignacion.DateFechaApertura,

                DateFechaCierre = asignacion.DateFechaCierre,

                DateTimeFechaRegistro = asignacion.DateTimeFechaRegistro,

                IdUsuarioRegistro = asignacion.IdUsuarioRegistro,

                BitActivo = asignacion.BitActivo
            };

            return ResponseFactory.Success(
                dto,
                "Asignación obtenida correctamente.");
        }
    }
}
