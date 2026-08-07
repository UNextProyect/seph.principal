using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatPreguntaAnalisis.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.CreateCatPreguntaAnalisis
{
    /// <summary>
    /// Procesa la creación de una pregunta
    /// para el análisis estratégico.
    /// </summary>
    public sealed class CreateCatPreguntaAnalisisCommandHandler(
        ICatPreguntaAnalisisRepository catPreguntaAnalisisRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            CreateCatPreguntaAnalisisCommand,
            ResponseWrapper<CatPreguntaAnalisisDto>>
    {
        /// <summary>
        /// Registra una nueva pregunta
        /// dentro del catálogo.
        /// </summary>
        public async Task<ResponseWrapper<CatPreguntaAnalisisDto>> Handle(
            CreateCatPreguntaAnalisisCommand request,
            CancellationToken cancellationToken)
        {
            /*
             * Elimina espacios innecesarios
             * al inicio y al final del texto.
             */
            var strPregunta =
                request.StrPregunta.Trim();

            /*
             * Evita registrar dos preguntas
             * con exactamente el mismo texto.
             */
            var exists =
                await catPreguntaAnalisisRepository
                    .ExistsByPreguntaAsync(
                        strPregunta,
                        cancellationToken);

            if (exists)
            {
                return ResponseFactory
                    .Failure<CatPreguntaAnalisisDto>(
                        "Ya existe una pregunta de análisis con el mismo texto.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene el siguiente orden disponible
             * sin modificar el orden de preguntas anteriores.
             */
            var intOrden =
                await catPreguntaAnalisisRepository
                    .GetNextOrdenAsync(
                        cancellationToken);

            var catPreguntaAnalisis =
                new Domain.Entities.CatPreguntaAnalisis
                {
                    StrPregunta =
                        strPregunta,

                    DateTimeFechaRegistro =
                        DateTime.Now,

                    BitActivo =
                        true,

                    IntOrden =
                        intOrden
                };

            await catPreguntaAnalisisRepository.AddAsync(
                catPreguntaAnalisis,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            await bitacoraService.RegistrarAsync(
                "PreguntaAnalisis",
                catPreguntaAnalisis.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString()
                    ?? "desconocido",
                currentUserService.Email?.ToString()
                    ?? "desconocido",
                catPreguntaAnalisis,
                cancellationToken);

            var dto =
                new CatPreguntaAnalisisDto(
                    catPreguntaAnalisis.Id,
                    catPreguntaAnalisis.StrPregunta,
                    catPreguntaAnalisis.DateTimeFechaRegistro,
                    catPreguntaAnalisis.BitActivo,
                    catPreguntaAnalisis.IntOrden);

            return ResponseFactory.Success(
                dto,
                "Pregunta de análisis registrada correctamente");
        }
    }
}
