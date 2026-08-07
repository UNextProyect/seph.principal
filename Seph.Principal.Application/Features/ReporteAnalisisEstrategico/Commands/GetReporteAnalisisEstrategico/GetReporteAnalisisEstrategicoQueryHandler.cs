using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteAnalisisEstrategico.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.GetReporteAnalisisEstrategico
{
    /// <summary>
    /// Procesa la consulta de un reporte
    /// de análisis estratégico.
    /// </summary>
    public sealed class GetReporteAnalisisEstrategicoQueryHandler(
        IReporteAnalisisEstrategicoRepository
            reporteAnalisisEstrategicoRepository,
        IRespuestaAnalisisRepository
            respuestaAnalisisRepository,
        ICatPreguntaAnalisisRepository
            catPreguntaAnalisisRepository)
        : IRequestHandler<
            GetReporteAnalisisEstrategicoQuery,
            ResponseWrapper<ReporteAnalisisEstrategicoDto>>
    {
        /// <summary>
        /// Obtiene las preguntas disponibles y las respuestas
        /// registradas para una institución y periodo.
        /// </summary>
        public async Task<
            ResponseWrapper<ReporteAnalisisEstrategicoDto>> Handle(
                GetReporteAnalisisEstrategicoQuery request,
                CancellationToken cancellationToken)
        {
            /*
             * Consulta todas las preguntas para poder combinar
             * las activas con aquellas que tengan historial.
             */
            var preguntasAnalisis =
                await catPreguntaAnalisisRepository
                    .GetAllAsync(
                        cancellationToken);

            // Busca el reporte correspondiente al periodo.
            var reporteAnalisisEstrategico =
                await reporteAnalisisEstrategicoRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            /*
             * Si todavía no existe un reporte, devuelve
             * las preguntas activas con respuestas vacías.
             */
            if (reporteAnalisisEstrategico is null)
            {
                var preguntasActivas =
                    preguntasAnalisis
                        .Where(
                            pregunta =>
                                pregunta.BitActivo)
                        .Select(
                            pregunta =>
                                new RespuestaAnalisisDto
                                {
                                    Id =
                                        0,

                                    IdPreguntaAnalisis =
                                        pregunta.Id,

                                    StrPregunta =
                                        pregunta.StrPregunta,

                                    StrRespuesta =
                                        null
                                })
                        .ToList();

                var dtoSinReporte =
                    new ReporteAnalisisEstrategicoDto
                    {
                        Id =
                            0,

                        IdMapInstitucionPeriodo =
                            request.IdMapInstitucionPeriodo,

                        RespuestasAnalisis =
                            preguntasActivas
                    };

                return ResponseFactory.Success(
                    dtoSinReporte,
                    "Preguntas de análisis obtenidas correctamente");
            }

            // Obtiene las respuestas registradas para el reporte.
            var respuestasRegistradas =
                await respuestaAnalisisRepository
                    .GetByIdAnalisisEstrategicoAsync(
                        reporteAnalisisEstrategico.Id,
                        cancellationToken);

            var respuestasPorPregunta =
                respuestasRegistradas.ToDictionary(
                    respuesta =>
                        respuesta.IdPreguntaAnalisis);

            /*
             * Incluye:
             * - Todas las preguntas activas.
             * - Preguntas inactivas que ya tengan historial.
             */
            var respuestasAnalisis =
                preguntasAnalisis
                    .Where(
                        pregunta =>
                            pregunta.BitActivo
                            || respuestasPorPregunta.ContainsKey(
                                pregunta.Id))
                    .Select(
                        pregunta =>
                        {
                            respuestasPorPregunta.TryGetValue(
                                pregunta.Id,
                                out var respuestaRegistrada);

                            return new RespuestaAnalisisDto
                            {
                                Id =
                                    respuestaRegistrada?.Id
                                    ?? 0,

                                IdPreguntaAnalisis =
                                    pregunta.Id,

                                /*
                                 * Las preguntas activas muestran
                                 * el texto vigente del catálogo.
                                 *
                                 * Las inactivas conservan el texto
                                 * histórico guardado en la respuesta.
                                 */
                                StrPregunta =
                                    pregunta.BitActivo
                                        ? pregunta.StrPregunta
                                        : respuestaRegistrada!
                                            .StrPregunta,

                                StrRespuesta =
                                    respuestaRegistrada?
                                        .StrRespuesta
                            };
                        })
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
                        respuestasAnalisis
                };

            return ResponseFactory.Success(
                dto,
                "Reporte de análisis estratégico obtenido correctamente");
        }
    }
}
