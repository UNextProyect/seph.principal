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

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.UpdateCatPreguntaAnalisis
{
    /// <summary>
    /// Procesa la actualización de una pregunta
    /// del catálogo de análisis estratégico.
    /// </summary>
    public sealed class UpdateCatPreguntaAnalisisCommandHandler(
        ICatPreguntaAnalisisRepository catPreguntaAnalisisRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            UpdateCatPreguntaAnalisisCommand,
            ResponseWrapper<CatPreguntaAnalisisDto>>
    {
        /// <summary>
        /// Actualiza el texto de una pregunta existente.
        /// </summary>
        public async Task<ResponseWrapper<CatPreguntaAnalisisDto>> Handle(
            UpdateCatPreguntaAnalisisCommand request,
            CancellationToken cancellationToken)
        {
            /*
             * Busca la pregunta con seguimiento habilitado
             * para permitir su modificación.
             */
            var catPreguntaAnalisis =
                await catPreguntaAnalisisRepository.GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (catPreguntaAnalisis is null)
            {
                return ResponseFactory
                    .Failure<CatPreguntaAnalisisDto>(
                        "No se encontró la pregunta de análisis.",
                        HttpStatusCode.NotFound);
            }

            /*
             * Elimina espacios innecesarios
             * al inicio y al final del texto.
             */
            var strPregunta =
                request.StrPregunta.Trim();

            /*
             * Verifica que otra pregunta distinta
             * no tenga el mismo texto.
             */
            var exists =
                await catPreguntaAnalisisRepository
                    .ExistsByPreguntaExceptIdAsync(
                        strPregunta,
                        request.Id,
                        cancellationToken);

            if (exists)
            {
                return ResponseFactory
                    .Failure<CatPreguntaAnalisisDto>(
                        "Ya existe una pregunta de análisis con el mismo texto.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Actualiza únicamente el texto.
             * El orden, estado y fecha original se conservan.
             */
            catPreguntaAnalisis.StrPregunta =
                strPregunta;

            catPreguntaAnalisisRepository.Update(
                catPreguntaAnalisis);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            await bitacoraService.RegistrarAsync(
                "PreguntaAnalisis",
                catPreguntaAnalisis.Id.ToString(),
                "Editar",
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
                "Pregunta de análisis actualizada correctamente");
        }
    }
}
