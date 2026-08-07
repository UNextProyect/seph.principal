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

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.UpdateReporteAnalisisEstrategico
{
    /// <summary>
    /// Procesa la actualización de un reporte
    /// de análisis estratégico existente.
    /// </summary>
    public sealed class UpdateReporteAnalisisEstrategicoCommandHandler(
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
            UpdateReporteAnalisisEstrategicoCommand,
            ResponseWrapper<ReporteAnalisisEstrategicoDto>>
    {
        /// <summary>
        /// Actualiza las respuestas registradas
        /// y agrega aquellas que todavía no existen.
        /// </summary>
        public async Task<
            ResponseWrapper<ReporteAnalisisEstrategicoDto>> Handle(
                UpdateReporteAnalisisEstrategicoCommand request,
                CancellationToken cancellationToken)
        {
            // Busca el reporte asociado a la institución y periodo.
            var reporteAnalisisEstrategico =
                await reporteAnalisisEstrategicoRepository
                    .GetByMapInstitucionPeriodoForUpdateAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (reporteAnalisisEstrategico is null)
            {
                return ResponseFactory
                    .Failure<ReporteAnalisisEstrategicoDto>(
                        "No existe un reporte de análisis estratégico para actualizar.",
                        HttpStatusCode.NotFound);
            }

            // Obtiene las respuestas existentes con seguimiento habilitado.
            var respuestasExistentes =
                await respuestaAnalisisRepository
                    .GetByIdAnalisisEstrategicoForUpdateAsync(
                        reporteAnalisisEstrategico.Id,
                        cancellationToken);

            // Consulta las preguntas disponibles actualmente.
            var preguntasActivas =
                await catPreguntaAnalisisRepository
                    .GetActiveAsync(
                        cancellationToken);

            var respuestasPorPregunta =
                respuestasExistentes.ToDictionary(
                    respuesta =>
                        respuesta.IdPreguntaAnalisis);

            var preguntasActivasPorId =
                preguntasActivas.ToDictionary(
                    pregunta =>
                        pregunta.Id);

            /*
             * Una pregunta es válida cuando continúa activa
             * o cuando ya posee una respuesta histórica.
             */
            var containsInvalidQuestion =
                request.RespuestasAnalisis.Any(
                    respuesta =>
                        !preguntasActivasPorId.ContainsKey(
                            respuesta.IdPreguntaAnalisis)
                        && !respuestasPorPregunta.ContainsKey(
                            respuesta.IdPreguntaAnalisis));

            if (containsInvalidQuestion)
            {
                return ResponseFactory
                    .Failure<ReporteAnalisisEstrategicoDto>(
                        "Una o más preguntas no existen o no se encuentran disponibles.",
                        HttpStatusCode.BadRequest);
            }

            var fechaRegistro =
                DateTime.Now;

            var nuevasRespuestas =
                new List<RespuestaAnalisis>();

            foreach (var respuestaRequest
                in request.RespuestasAnalisis)
            {
                /*
                 * Si la respuesta ya existe, únicamente
                 * se actualiza el texto capturado.
                 */
                if (respuestasPorPregunta.TryGetValue(
                    respuestaRequest.IdPreguntaAnalisis,
                    out var respuestaExistente))
                {
                    respuestaExistente.StrRespuesta =
                        string.IsNullOrWhiteSpace(
                            respuestaRequest.StrRespuesta)
                            ? null
                            : respuestaRequest.StrRespuesta;

                    /*
                     * No se modifica StrPregunta porque contiene
                     * la copia histórica del texto original.
                     */
                    respuestaAnalisisRepository.Update(
                        respuestaExistente);

                    continue;
                }

                /*
                 * Las respuestas nuevas solo pueden crearse
                 * para preguntas que continúan activas.
                 */
                if (string.IsNullOrWhiteSpace(
                    respuestaRequest.StrRespuesta))
                {
                    continue;
                }

                var pregunta =
                    preguntasActivasPorId[
                        respuestaRequest.IdPreguntaAnalisis];

                var nuevaRespuesta =
                    new RespuestaAnalisis
                    {
                        IdAnalisisEstrategico =
                            reporteAnalisisEstrategico.Id,

                        IdPreguntaAnalisis =
                            pregunta.Id,

                        DateTimeFechaRegistro =
                            fechaRegistro,

                        StrRespuesta =
                            respuestaRequest.StrRespuesta,

                        // Guarda la copia histórica de la pregunta.
                        StrPregunta =
                            pregunta.StrPregunta
                    };

                nuevasRespuestas.Add(
                    nuevaRespuesta);
            }

            if (nuevasRespuestas.Count > 0)
            {
                await respuestaAnalisisRepository.AddRangeAsync(
                    nuevasRespuestas,
                    cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            var respuestasActualizadas =
                respuestasExistentes
                    .Concat(nuevasRespuestas)
                    .OrderBy(
                        respuesta =>
                            respuesta.Id)
                    .ToList();

            var dto =
                new ReporteAnalisisEstrategicoDto
                {
                    Id =
                        reporteAnalisisEstrategico.Id,

                    IdMapInstitucionPeriodo =
                        reporteAnalisisEstrategico
                            .IdMapInstitucionPeriodo,

                    RespuestasAnalisis =
                        respuestasActualizadas
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

            await bitacoraService.RegistrarAsync(
                "AnalisisEstrategico",
                reporteAnalisisEstrategico.Id.ToString(),
                "Editar",
                currentUserService.UserId?.ToString()
                    ?? "desconocido",
                currentUserService.Email?.ToString()
                    ?? "desconocido",
                dto,
                cancellationToken);

            return ResponseFactory.Success(
                dto,
                "Reporte de análisis estratégico actualizado correctamente");
        }
    }
}
