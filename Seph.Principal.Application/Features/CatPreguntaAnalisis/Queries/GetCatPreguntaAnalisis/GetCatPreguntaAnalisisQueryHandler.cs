using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Queries.GetCatPreguntaAnalisis
{
    /// <summary>
    /// Obtiene todas las preguntas registradas
    /// para el análisis estratégico.
    /// </summary>
    public sealed class GetCatPreguntaAnalisisQueryHandler(
        ICatPreguntaAnalisisRepository
            catPreguntaAnalisisRepository)
        : IRequestHandler<
            GetCatPreguntaAnalisisQuery,
            ResponseWrapper<
                IReadOnlyList<CatPreguntaAnalisisDto>>>
    {
        /// <summary>
        /// Consulta las preguntas activas e inactivas
        /// registradas dentro del catálogo.
        /// </summary>
        public async Task<
            ResponseWrapper<
                IReadOnlyList<CatPreguntaAnalisisDto>>> Handle(
                    GetCatPreguntaAnalisisQuery request,
                    CancellationToken cancellationToken)
        {
            // Consulta todas las preguntas registradas.
            var catPreguntasAnalisis =
                await catPreguntaAnalisisRepository
                    .GetAllAsync(
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
                "Catálogo de preguntas de análisis obtenido correctamente");
        }
    }
}
