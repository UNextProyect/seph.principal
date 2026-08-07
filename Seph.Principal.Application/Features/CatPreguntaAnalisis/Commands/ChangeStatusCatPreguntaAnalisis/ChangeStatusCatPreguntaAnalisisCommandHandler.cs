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

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.ChangeStatusCatPreguntaAnalisis
{
    /// <summary>
    /// Procesa el cambio de estado de una pregunta
    /// del catálogo de análisis estratégico.
    /// </summary>
    public sealed class ChangeStatusCatPreguntaAnalisisCommandHandler(
        ICatPreguntaAnalisisRepository catPreguntaAnalisisRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            ChangeStatusCatPreguntaAnalisisCommand,
            ResponseWrapper<CatPreguntaAnalisisDto>>
    {
        /// <summary>
        /// Activa o desactiva una pregunta existente.
        /// </summary>
        public async Task<ResponseWrapper<CatPreguntaAnalisisDto>> Handle(
            ChangeStatusCatPreguntaAnalisisCommand request,
            CancellationToken cancellationToken)
        {
            /*
             * Busca la pregunta con seguimiento habilitado
             * para permitir el cambio de estado.
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
             * Modifica únicamente el estado.
             * El texto, fecha y orden permanecen sin cambios.
             */
            catPreguntaAnalisis.BitActivo =
                request.BitActivo;

            catPreguntaAnalisisRepository.Update(
                catPreguntaAnalisis);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            var accion =
                request.BitActivo
                    ? "Activar"
                    : "Desactivar";

            await bitacoraService.RegistrarAsync(
                "PreguntaAnalisis",
                catPreguntaAnalisis.Id.ToString(),
                accion,
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

            var mensaje =
                request.BitActivo
                    ? "Pregunta de análisis activada correctamente"
                    : "Pregunta de análisis desactivada correctamente";

            return ResponseFactory.Success(
                dto,
                mensaje);
        }
    }
}
