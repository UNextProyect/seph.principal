using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Queries.GetActiveCatPreguntaAnalisis
{
    /// <summary>
    /// Obtiene las preguntas activas disponibles
    /// para la captura del análisis estratégico.
    /// </summary>
    public sealed class GetActiveCatPreguntaAnalisisQueryHandler(
        ICatPreguntaAnalisisRepository
            catPreguntaAnalisisRepository)
        : IRequestHandler<
            GetActiveCatPreguntaAnalisisQuery,
            ResponseWrapper<
                IReadOnlyList<CatPreguntaAnalisisDto>>>
    {
        /// <summary>
        /// Consulta únicamente las preguntas
        /// que se encuentran activas.
        /// </summary>
        public async Task<
            ResponseWrapper<
                IReadOnlyList<CatPreguntaAnalisisDto>>> Handle(
                    GetActiveCatPreguntaAnalisisQuery request,
                    CancellationToken cancellationToken)
        {
            // Consulta únicamente las preguntas activas.
            var catPreguntasAnalisis =
                await catPreguntaAnalisisRepository
                    .GetActiveAsync(
                        cancellationToken);

            // Convierte las entidades del dominio a DTO.
            IReadOnlyList<CatPreguntaAnalisisDto> response =
                catPreguntasAnalisis
                    .Select(
                        pregunta =>
                            new CatPreguntaAnalisisDto(
                                pregunta.Id,
                                pregunta.StrPregunta,
                                pregunta.DateTimeFechaRegistro,
                                pregunta.BitActivo,
                                pregunta.IntOrden))
                    .ToList();

            return ResponseFactory.Success(
                response,
                "Preguntas activas de análisis obtenidas correctamente");
        }
    }
}
