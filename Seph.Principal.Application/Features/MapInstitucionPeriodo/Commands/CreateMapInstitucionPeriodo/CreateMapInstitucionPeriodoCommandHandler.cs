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

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.CreateMapInstitucionPeriodo
{
    /// <summary>
    /// Procesa la asignación de un periodo
    /// a una institución.
    /// </summary>
    public sealed class CreateMapInstitucionPeriodoCommandHandler(
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<
            CreateMapInstitucionPeriodoCommand,
            ResponseWrapper<MapInstitucionPeriodoDto>>
    {
        public async Task<ResponseWrapper<MapInstitucionPeriodoDto>> Handle(
            CreateMapInstitucionPeriodoCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos veces el mismo periodo
            // para la misma institución.
            var asignacionExistente =
                await mapInstitucionPeriodoRepository
                    .GetByInstitucionPeriodoAsync(
                        request.IdInstitucion,
                        request.IdPeriodo,
                        cancellationToken);

            if (asignacionExistente is not null)
            {
                return ResponseFactory
                    .Failure<MapInstitucionPeriodoDto>(
                        "La institución ya tiene registrado este periodo.",
                        HttpStatusCode.BadRequest);
            }

            var mapInstitucionPeriodo =
                new Domain.Entities.MapInstitucionPeriodo
                {
                    IdInstitucion =
                        request.IdInstitucion,

                    IdPeriodo =
                        request.IdPeriodo,

                    BitCapturaAbierta =
                        request.BitCapturaAbierta,

                    DateFechaApertura =
                        request.DateFechaApertura,

                    DateFechaCierre =
                        request.DateFechaCierre,

                    DateTimeFechaRegistro =
                        DateTime.Now,

                    IdUsuarioRegistro =
                        request.IdUsuarioRegistro,

                    BitActivo = true
                };

            await mapInstitucionPeriodoRepository.AddAsync(
                mapInstitucionPeriodo,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            /*
             Vuelve a consultar la asignación para cargar
             Institución, Periodo y TipoPeriodo.
            */
            var asignacionCreada =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        mapInstitucionPeriodo.Id,
                        cancellationToken);

            if (asignacionCreada is null)
            {
                return ResponseFactory
                    .Failure<MapInstitucionPeriodoDto>(
                        "La asignación fue registrada, pero no pudo consultarse.",
                        HttpStatusCode.InternalServerError);
            }

            var dto = new MapInstitucionPeriodoDto
            {
                Id =
                    asignacionCreada.Id,

                IdInstitucion =
                    asignacionCreada.IdInstitucion,

                StrInstitucion =
                    asignacionCreada.Institucion.StrNombre,

                StrSiglasInstitucion =
                    asignacionCreada.Institucion.StrSiglas,

                IdPeriodo =
                    asignacionCreada.IdPeriodo,

                StrPeriodo =
                    asignacionCreada.Periodo.StrValor,

                StrDescripcionPeriodo =
                    asignacionCreada.Periodo.StrDescripcion,

                IntAnio =
                    asignacionCreada.Periodo.IntAnio,

                IntNumeroPeriodo =
                    asignacionCreada.Periodo.IntNumeroPeriodo,

                DateFechaInicioPeriodo =
                    asignacionCreada.Periodo.DateFechaInicio,

                DateFechaFinPeriodo =
                    asignacionCreada.Periodo.DateFechaFin,

                IdTipoPeriodo =
                    asignacionCreada.Periodo.IdTipoPeriodo,

                StrTipoPeriodo =
                    asignacionCreada.Periodo.TipoPeriodo.StrValor,

                BitCapturaAbierta =
                    asignacionCreada.BitCapturaAbierta,

                DateFechaApertura =
                    asignacionCreada.DateFechaApertura,

                DateFechaCierre =
                    asignacionCreada.DateFechaCierre,

                DateTimeFechaRegistro =
                    asignacionCreada.DateTimeFechaRegistro,

                IdUsuarioRegistro =
                    asignacionCreada.IdUsuarioRegistro,

                BitActivo =
                    asignacionCreada.BitActivo
            };

            return ResponseFactory.Success(
                dto,
                "Periodo asignado a la institución correctamente.");
        }
    }
}
