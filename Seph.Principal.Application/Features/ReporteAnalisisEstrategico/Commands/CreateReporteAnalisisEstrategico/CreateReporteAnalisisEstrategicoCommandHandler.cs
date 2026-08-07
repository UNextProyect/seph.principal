using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.CreateReporteAnalisisEstrategico
{
    /// <summary>
    /// Procesa la creación de un reporte
    /// de análisis estratégico.
    /// </summary>
    public sealed class CreateReporteAnalisisEstrategicoCommandHandler(
        IReporteAnalisisEstrategicoRepository
            reporteAnalisisEstrategicoRepository,
        IRespuestaAnalisisRepository
            respuestaAnalisisRepository,
        ICatPreguntaAnalisisRepository
            catPreguntaAnalisisRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            CreateReporteAnalisisEstrategicoCommand,
            ResponseWrapper<ReporteAnalisisEstrategicoDto>>
    {
        /// <summary>
        /// Registra el reporte y las respuestas
        /// capturadas para una institución y periodo.
        /// </summary>
        public async Task<
            ResponseWrapper<ReporteAnalisisEstrategicoDto>> Handle(
                CreateReporteAnalisisEstrategicoCommand request,
                CancellationToken cancellationToken)
        {
            // Evita registrar dos reportes para el mismo periodo.
            var exists =
                await reporteAnalisisEstrategicoRepository
                    .ExistsByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (exists)
            {
                return ResponseFactory
                    .Failure<ReporteAnalisisEstrategicoDto>(
                        "Ya existe un reporte de análisis estratégico registrado para este periodo.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Consulta las preguntas activas para validar
             * que las respuestas correspondan al catálogo.
             */
            var preguntasActivas =
                await catPreguntaAnalisisRepository
                    .GetActiveAsync(
                        cancellationToken);

            var preguntasPorId =
                preguntasActivas.ToDictionary(
                    pregunta => pregunta.Id);

            var containsInvalidQuestion =
                request.RespuestasAnalisis.Any(
                    respuesta =>
                        !preguntasPorId.ContainsKey(
                            respuesta.IdPreguntaAnalisis));

            if (containsInvalidQuestion)
            {
                return ResponseFactory
                    .Failure<ReporteAnalisisEstrategicoDto>(
                        "Una o más preguntas no existen o no se encuentran activas.",
                        HttpStatusCode.BadRequest);
            }

            var fechaRegistro =
                DateTime.Now;

            var reporteAnalisisEstrategico =
                new Domain.Entities.ReporteAnalisisEstrategico
                {
                    IdMapInstitucionPeriodo =
                        request.IdMapInstitucionPeriodo,

                    DateTimeFechaRegistro =
                        fechaRegistro,

                    IdUsuarioRegistro =
                        request.IdUsuarioRegistro,

                    BitActivo =
                        true
                };

            await reporteAnalisisEstrategicoRepository.AddAsync(
                reporteAnalisisEstrategico,
                cancellationToken);

            // Guarda primero el reporte para obtener su identificador.
            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            /*
             * Solo registra respuestas que contienen texto.
             * Las preguntas sin respuesta permanecen disponibles
             * para una actualización posterior.
             */
            var respuestasAnalisis =
                request.RespuestasAnalisis
                    .Where(
                        respuesta =>
                            !string.IsNullOrWhiteSpace(
                                respuesta.StrRespuesta))
                    .Select(
                        respuesta =>
                        {
                            var pregunta =
                                preguntasPorId[
                                    respuesta.IdPreguntaAnalisis];

                            return new RespuestaAnalisis
                            {
                                IdAnalisisEstrategico =
                                    reporteAnalisisEstrategico.Id,

                                IdPreguntaAnalisis =
                                    pregunta.Id,

                                DateTimeFechaRegistro =
                                    fechaRegistro,

                                StrRespuesta =
                                    respuesta.StrRespuesta,

                                /*
                                 * Conserva una copia del texto
                                 * que tenía la pregunta al responderse.
                                 */
                                StrPregunta =
                                    pregunta.StrPregunta
                            };
                        })
                    .ToList();

            if (respuestasAnalisis.Count > 0)
            {
                await respuestaAnalisisRepository.AddRangeAsync(
                    respuestasAnalisis,
                    cancellationToken);

                await unitOfWork.SaveChangesAsync(
                    cancellationToken);
            }

            await bitacoraService.RegistrarAsync(
                "AnalisisEstrategico",
                reporteAnalisisEstrategico.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString()
                    ?? "desconocido",
                currentUserService.Email?.ToString()
                    ?? "desconocido",
                reporteAnalisisEstrategico,
                cancellationToken);

            var dto =
                new ReporteAnalisisEstrategicoDto
                {
                    Id =
                        reporteAnalisisEstrategico.Id,

                    IdMapInstitucionPeriodo =
                        reporteAnalisisEstrategico
                            .IdMapInstitucionPeriodo,

                    RespuestasAnalisis =
                        respuestasAnalisis
                            .Select(
                                respuesta =>
                                    new RespuestaAnalisisDto
                                    {
                                        Id =
                                            respuesta.Id,

                                        IdPreguntaAnalisis =
                                            respuesta
                                                .IdPreguntaAnalisis,

                                        StrPregunta =
                                            respuesta.StrPregunta,

                                        StrRespuesta =
                                            respuesta.StrRespuesta
                                    })
                            .ToList()
                };

            return ResponseFactory.Success(
                dto,
                "Reporte de análisis estratégico registrado correctamente");
        }
    }
}
