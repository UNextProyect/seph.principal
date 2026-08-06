using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Linq;
using System.Net;

namespace Seph.Principal.Application.Features.ReporteVinculacion.Commands
{
    /// <summary>
    /// Procesa la actualización de un reporte de vinculación.
    /// </summary>
    public sealed class UpdateReporteVinculacionCommandHandler(
        IReporteVinculacionRepository reporteVinculacionRepository,
        ISectorVinculadoVinculacionRepository sectorVinculadoVinculacionRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            UpdateReporteVinculacionCommand,
            ResponseWrapper<ReporteVinculacionDto>>
    {
        /// <summary>
        /// Actualiza la información capturable de un reporte de vinculación.
        /// </summary>
        public async Task<ResponseWrapper<ReporteVinculacionDto>> Handle(
            UpdateReporteVinculacionCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte asociado a la institución y periodo.
            var reporte = await reporteVinculacionRepository
                .GetByMapInstitucionPeriodoForUpdateAsync(
                    request.IdMapInstitucionPeriodo,
                    cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteVinculacionDto>(
                    "No existe un reporte de vinculación para actualizar.",
                    HttpStatusCode.NotFound);
            }

            // Actualiza únicamente la información capturable.
            reporte.IntTotalConveniosActivos =
                request.IntTotalConveniosActivos;

            reporte.BitPracticasProfesionales =
                request.BitPracticasProfesionales;

            reporte.BitServicioSocial =
                request.BitServicioSocial;

            reporte.BitSeguimientoEgresados =
                request.BitSeguimientoEgresados;

            reporte.IdMecanismoSeguimiento =
                request.BitSeguimientoEgresados
                    ? request.IdMecanismoSeguimiento
                    : null;

            reporte.DecimalPorcentajeLaborando =
                request.DecimalPorcentajeLaborando;

            // Elimina los sectores registrados anteriormente.
            await sectorVinculadoVinculacionRepository
                .DeleteByIdVinculacionAsync(
                    reporte.Id,
                    cancellationToken);

            // Prepara nuevamente los sectores enviados en la solicitud.
            var sectoresVinculados =
                request.SectoresVinculados
                    .Select(
                        sector =>
                            new SectorVinculadoVinculacion
                            {
                                IdVinculacion =
                                    reporte.Id,

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
                reporte.Id.ToString(),
                "Editar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporte,
                cancellationToken);

            var dto =
                new ReporteVinculacionDto
                {
                    Id =
                        reporte.Id,

                    IdMapInstitucionPeriodo =
                        reporte.IdMapInstitucionPeriodo,

                    IntTotalConveniosActivos =
                        reporte.IntTotalConveniosActivos,

                    BitPracticasProfesionales =
                        reporte.BitPracticasProfesionales,

                    BitServicioSocial =
                        reporte.BitServicioSocial,

                    BitSeguimientoEgresados =
                        reporte.BitSeguimientoEgresados,

                    IdMecanismoSeguimiento =
                        reporte.IdMecanismoSeguimiento,

                    DecimalPorcentajeLaborando =
                        reporte.DecimalPorcentajeLaborando,

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
                "Reporte de vinculación actualizado correctamente");
        }
    }
}