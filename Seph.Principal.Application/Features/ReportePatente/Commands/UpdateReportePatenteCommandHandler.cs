using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Linq;
using System.Net;

namespace Seph.Principal.Application.Features.ReportePatente.Commands
{
    /// <summary>
    /// Procesa la actualización de un reporte de patente.
    /// </summary>
    public sealed class UpdateReportePatenteCommandHandler(
        IReportePatenteRepository reportePatenteRepository,
        IInventorPatenteRepository inventorPatenteRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            UpdateReportePatenteCommand,
            ResponseWrapper<ReportePatenteDto>>
    {
        /// <summary>
        /// Actualiza la información capturable
        /// de un reporte de patente.
        /// </summary>
        public async Task<ResponseWrapper<ReportePatenteDto>> Handle(
            UpdateReportePatenteCommand request,
            CancellationToken cancellationToken)
        {
            // Busca la patente mediante su identificador.
            var reporte =
                await reportePatenteRepository
                    .GetByIdForUpdateAsync(
                        request.Id,
                        cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReportePatenteDto>(
                    "No existe un reporte de patente para actualizar.",
                    HttpStatusCode.NotFound);
            }

            // Verifica que la patente corresponda
            // a la institución y periodo enviados.
            if (reporte.IdMapInstitucionPeriodo !=
                request.IdMapInstitucionPeriodo)
            {
                return ResponseFactory.Failure<ReportePatenteDto>(
                    "La patente no corresponde al periodo institucional indicado.",
                    HttpStatusCode.BadRequest);
            }

            // Valida que el nuevo número de registro o solicitud
            // no se encuentre asignado a otra patente.
            if (reporte.StrNumeroRegistroSolicitud !=
                request.StrNumeroRegistroSolicitud)
            {
                var exists =
                    await reportePatenteRepository
                        .ExistsByNumeroRegistroSolicitudAsync(
                            request.StrNumeroRegistroSolicitud,
                            cancellationToken);

                if (exists)
                {
                    return ResponseFactory.Failure<ReportePatenteDto>(
                        "Ya existe una patente registrada con este número de registro o solicitud.",
                        HttpStatusCode.BadRequest);
                }
            }

            // Actualiza únicamente la información capturable.
            reporte.StrNombreTitulo =
                request.StrNombreTitulo;

            reporte.StrNumeroRegistroSolicitud =
                request.StrNumeroRegistroSolicitud;

            reporte.IdTipoPatente =
                request.IdTipoPatente;

            reporte.IdEstatusPatente =
                request.IdEstatusPatente;

            reporte.DateFechaSolicitud =
                request.DateFechaSolicitud;

            reporte.DateFechaConcesion =
                request.DateFechaConcesion;

            reporte.StrTitularPatente =
                request.StrTitularPatente;

            // Elimina los inventores registrados anteriormente.
            await inventorPatenteRepository
                .DeleteByIdPatenteAsync(
                    reporte.Id,
                    cancellationToken);

            // Prepara nuevamente los inventores
            // enviados en la solicitud.
            var inventores =
                request.Inventores
                    .Select(
                        inventor =>
                            new InventorPatente
                            {
                                IdPatente =
                                    reporte.Id,

                                StrNombreCompleto =
                                    inventor.StrNombreCompleto
                            })
                    .ToList();

            await inventorPatenteRepository.AddRangeAsync(
                inventores,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            await bitacoraService.RegistrarAsync(
                "Patente",
                reporte.Id.ToString(),
                "Editar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporte,
                cancellationToken);

            var dto =
                new ReportePatenteDto
                {
                    Id =
                        reporte.Id,

                    IdMapInstitucionPeriodo =
                        reporte.IdMapInstitucionPeriodo,

                    StrNombreTitulo =
                        reporte.StrNombreTitulo,

                    StrNumeroRegistroSolicitud =
                        reporte.StrNumeroRegistroSolicitud,

                    IdTipoPatente =
                        reporte.IdTipoPatente,

                    IdEstatusPatente =
                        reporte.IdEstatusPatente,

                    DateFechaSolicitud =
                        reporte.DateFechaSolicitud,

                    DateFechaConcesion =
                        reporte.DateFechaConcesion,

                    StrTitularPatente =
                        reporte.StrTitularPatente,

                    Inventores =
                        inventores
                            .Select(
                                inventor =>
                                    new InventorPatenteDto
                                    {
                                        StrNombreCompleto =
                                            inventor.StrNombreCompleto
                                    })
                            .ToList()
                };

            return ResponseFactory.Success(
                dto,
                "Reporte de patente actualizado correctamente");
        }
    }
}