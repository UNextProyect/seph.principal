using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReportePatente.Commands
{
    /// <summary>
    /// Procesa la creación de un reporte de patente.
    /// </summary>
    public sealed class CreateReportePatenteCommandHandler(
        IReportePatenteRepository reportePatenteRepository,
        IInventorPatenteRepository inventorPatenteRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            CreateReportePatenteCommand,
            ResponseWrapper<ReportePatenteDto>>
    {
        /// <summary>
        /// Crea un reporte de patente para una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReportePatenteDto>> Handle(
            CreateReportePatenteCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos patentes con el mismo
            // número de registro o solicitud.
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

            var reportePatente =
                new Domain.Entities.ReportePatente
                {
                    IdMapInstitucionPeriodo =
                        request.IdMapInstitucionPeriodo,

                    StrNombreTitulo =
                        request.StrNombreTitulo,

                    StrNumeroRegistroSolicitud =
                        request.StrNumeroRegistroSolicitud,

                    IdTipoPatente =
                        request.IdTipoPatente,

                    IdEstatusPatente =
                        request.IdEstatusPatente,

                    DateFechaSolicitud =
                        request.DateFechaSolicitud,

                    DateFechaConcesion =
                        request.DateFechaConcesion,

                    StrTitularPatente =
                        request.StrTitularPatente,

                    DateTimeFechaRegistro =
                        DateTime.Now,

                    IdUsuarioRegistro =
                        request.IdUsuarioRegistro,

                    BitActivo =
                        true
                };

            await reportePatenteRepository.AddAsync(
                reportePatente,
                cancellationToken);

            // Guarda primero la patente para obtener su identificador.
            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            var inventores =
                request.Inventores
                    .Select(
                        inventor =>
                            new InventorPatente
                            {
                                IdPatente =
                                    reportePatente.Id,

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
                reportePatente.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reportePatente,
                cancellationToken);

            var dto =
                new ReportePatenteDto
                {
                    Id =
                        reportePatente.Id,

                    IdMapInstitucionPeriodo =
                        reportePatente.IdMapInstitucionPeriodo,

                    StrNombreTitulo =
                        reportePatente.StrNombreTitulo,

                    StrNumeroRegistroSolicitud =
                        reportePatente.StrNumeroRegistroSolicitud,

                    IdTipoPatente =
                        reportePatente.IdTipoPatente,

                    IdEstatusPatente =
                        reportePatente.IdEstatusPatente,

                    DateFechaSolicitud =
                        reportePatente.DateFechaSolicitud,

                    DateFechaConcesion =
                        reportePatente.DateFechaConcesion,

                    StrTitularPatente =
                        reportePatente.StrTitularPatente,

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
                "Reporte de patente registrado correctamente");
        }
    }
}