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

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.UpdateMapInstitucionPeriodo
{
    /// <summary>
    /// Actualiza una asignación de periodo
    /// correspondiente a una institución.
    /// </summary>
    public sealed class UpdateMapInstitucionPeriodoCommandHandler(
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<
            UpdateMapInstitucionPeriodoCommand,
            ResponseWrapper<MapInstitucionPeriodoDto>>
    {
        public async Task<ResponseWrapper<MapInstitucionPeriodoDto>> Handle(
            UpdateMapInstitucionPeriodoCommand request,
            CancellationToken cancellationToken)
        {
            // Busca la asignación que se desea modificar.
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

            // Comprueba que no exista otra asignación
            // con la misma institución y el mismo periodo.
            var asignacionDuplicada =
                await mapInstitucionPeriodoRepository
                    .GetByInstitucionPeriodoAsync(
                        request.IdInstitucion,
                        request.IdPeriodo,
                        cancellationToken);

            if (asignacionDuplicada is not null &&
                asignacionDuplicada.Id != request.Id)
            {
                return ResponseFactory.Failure<MapInstitucionPeriodoDto>(
                    "La institución ya tiene asignado este periodo.",
                    HttpStatusCode.Conflict);
            }

            // Actualiza solamente los campos permitidos.
            asignacion.IdInstitucion =
                request.IdInstitucion;

            asignacion.IdPeriodo =
                request.IdPeriodo;

            asignacion.BitCapturaAbierta =
                request.BitCapturaAbierta;

            asignacion.DateFechaApertura =
                request.DateFechaApertura;

            asignacion.DateFechaCierre =
                request.DateFechaCierre;

            mapInstitucionPeriodoRepository.Update(asignacion);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            // Consulta nuevamente la asignación para cargar
            // Institución, Periodo y TipoPeriodo.
            var asignacionActualizada =
                await mapInstitucionPeriodoRepository.GetByIdAsync(
                    asignacion.Id,
                    cancellationToken);

            if (asignacionActualizada is null)
            {
                return ResponseFactory.Failure<MapInstitucionPeriodoDto>(
                    "La asignación fue actualizada, pero no pudo consultarse nuevamente.",
                    HttpStatusCode.NotFound);
            }

            var dto = new MapInstitucionPeriodoDto
            {
                Id = asignacionActualizada.Id,

                IdInstitucion =
                    asignacionActualizada.IdInstitucion,

                StrInstitucion =
                    asignacionActualizada.Institucion.StrNombre,

                StrSiglasInstitucion =
                    asignacionActualizada.Institucion.StrSiglas,

                IdPeriodo =
                    asignacionActualizada.IdPeriodo,

                StrPeriodo =
                    asignacionActualizada.Periodo.StrValor,

                StrDescripcionPeriodo =
                    asignacionActualizada.Periodo.StrDescripcion,

                IntAnio =
                    asignacionActualizada.Periodo.IntAnio,

                IntNumeroPeriodo =
                    asignacionActualizada.Periodo.IntNumeroPeriodo,

                DateFechaInicioPeriodo =
                    asignacionActualizada.Periodo.DateFechaInicio,

                DateFechaFinPeriodo =
                    asignacionActualizada.Periodo.DateFechaFin,

                IdTipoPeriodo =
                    asignacionActualizada.Periodo.IdTipoPeriodo,

                StrTipoPeriodo =
                    asignacionActualizada.Periodo.TipoPeriodo.StrValor,

                BitCapturaAbierta =
                    asignacionActualizada.BitCapturaAbierta,

                DateFechaApertura =
                    asignacionActualizada.DateFechaApertura,

                DateFechaCierre =
                    asignacionActualizada.DateFechaCierre,

                DateTimeFechaRegistro =
                    asignacionActualizada.DateTimeFechaRegistro,

                IdUsuarioRegistro =
                    asignacionActualizada.IdUsuarioRegistro,

                BitActivo =
                    asignacionActualizada.BitActivo
            };

            return ResponseFactory.Success(
                dto,
                "Asignación de periodo actualizada correctamente.");
        }
    }

}
