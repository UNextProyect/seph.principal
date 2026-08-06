using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Commands
{
    /// <summary>
    /// Procesa la creación de un reporte de vinculación.
    /// </summary>
    public sealed class CreateReporteVinculacionCommandHandler(
        IReporteVinculacionRepository reporteVinculacionRepository,
        ISectorVinculadoVinculacionRepository sectorVinculadoVinculacionRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            CreateReporteVinculacionCommand,
            ResponseWrapper<ReporteVinculacionDto>>
    {
        /// <summary>
        /// Crea un reporte de vinculación para una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReporteVinculacionDto>> Handle(
            CreateReporteVinculacionCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos reportes para el mismo periodo institucional.
            var exists =
                await reporteVinculacionRepository
                    .ExistsByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<ReporteVinculacionDto>(
                    "Ya existe un reporte de vinculación registrado para este periodo.",
                    HttpStatusCode.BadRequest);
            }

            var reporteVinculacion =
                new Domain.Entities.ReporteVinculacion
                {
                    IdMapInstitucionPeriodo =
                        request.IdMapInstitucionPeriodo,

                    IntTotalConveniosActivos =
                        request.IntTotalConveniosActivos,

                    BitPracticasProfesionales =
                        request.BitPracticasProfesionales,

                    BitServicioSocial =
                        request.BitServicioSocial,

                    BitSeguimientoEgresados =
                        request.BitSeguimientoEgresados,

                    IdMecanismoSeguimiento =
                        request.BitSeguimientoEgresados
                            ? request.IdMecanismoSeguimiento
                            : null,

                    DecimalPorcentajeLaborando =
                        request.DecimalPorcentajeLaborando,

                    DateTimeFechaRegistro =
                        DateTime.Now,

                    IdUsuarioRegistro =
                        request.IdUsuarioRegistro,

                    BitActivo =
                        true
                };

            await reporteVinculacionRepository.AddAsync(
                reporteVinculacion,
                cancellationToken);

            // Guarda primero el reporte para obtener su identificador.
            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            var sectoresVinculados =
                request.SectoresVinculados
                    .Select(
                        sector =>
                            new SectorVinculadoVinculacion
                            {
                                IdVinculacion =
                                    reporteVinculacion.Id,

                                IdSectorVinculado =
                                    sector.IdSectorVinculado,

                                StrOtros =
                                    sector.StrOtros
                            })
                    .ToList();

            await sectorVinculadoVinculacionRepository.AddRangeAsync(
                sectoresVinculados,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            await bitacoraService.RegistrarAsync(
                "Vinculacion",
                reporteVinculacion.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporteVinculacion,
                cancellationToken);

            var dto =
                new ReporteVinculacionDto
                {
                    Id =
                        reporteVinculacion.Id,

                    IdMapInstitucionPeriodo =
                        reporteVinculacion.IdMapInstitucionPeriodo,

                    IntTotalConveniosActivos =
                        reporteVinculacion.IntTotalConveniosActivos,

                    BitPracticasProfesionales =
                        reporteVinculacion.BitPracticasProfesionales,

                    BitServicioSocial =
                        reporteVinculacion.BitServicioSocial,

                    BitSeguimientoEgresados =
                        reporteVinculacion.BitSeguimientoEgresados,

                    IdMecanismoSeguimiento =
                        reporteVinculacion.IdMecanismoSeguimiento,

                    DecimalPorcentajeLaborando =
                        reporteVinculacion.DecimalPorcentajeLaborando,

                    SectoresVinculados =
                        sectoresVinculados
                            .Select(
                                sector =>
                                    new SectorVinculadoDto
                                    {
                                        IdSectorVinculado =
                                            sector.IdSectorVinculado,

                                        StrOtros =
                                            sector.StrOtros
                                    })
                            .ToList()
                };

            return ResponseFactory.Success(
                dto,
                "Reporte de vinculación registrado correctamente");
        }
    }
}