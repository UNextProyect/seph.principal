using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportesPatenteByPeriodo
{
    /// <summary>
    /// Procesa la consulta de las patentes
    /// registradas durante un periodo institucional.
    /// </summary>
    public sealed class GetReportesPatenteByPeriodoQueryHandler(
        IReportePatenteRepository reportePatenteRepository,
        IInventorPatenteRepository inventorPatenteRepository)
        : IRequestHandler<
            GetReportesPatenteByPeriodoQuery,
            ResponseWrapper<
                IReadOnlyList<ReportePatenteDto>>>
    {
        /// <summary>
        /// Obtiene las patentes y sus inventores
        /// correspondientes al periodo indicado.
        /// </summary>
        public async Task<
            ResponseWrapper<
                IReadOnlyList<ReportePatenteDto>>>
            Handle(
                GetReportesPatenteByPeriodoQuery request,
                CancellationToken cancellationToken)
        {
            var reportes =
                await reportePatenteRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            var response =
                new List<ReportePatenteDto>();

            // Construye cada reporte con sus inventores.
            foreach (var reporte in reportes)
            {
                var inventores =
                    await inventorPatenteRepository
                        .GetByIdPatenteAsync(
                            reporte.Id,
                            cancellationToken);

                response.Add(
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
                    });
            }

            return ResponseFactory.Success<
                IReadOnlyList<ReportePatenteDto>>(
                response,
                "Reportes de patentes obtenidos correctamente");
        }
    }
}