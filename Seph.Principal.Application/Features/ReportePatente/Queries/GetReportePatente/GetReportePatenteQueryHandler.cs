using System.Linq;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatente
{
    /// <summary>
    /// Procesa la consulta de un reporte de patente.
    /// </summary>
    public sealed class GetReportePatenteQueryHandler(
        IReportePatenteRepository reportePatenteRepository,
        IInventorPatenteRepository inventorPatenteRepository)
        : IRequestHandler<
            GetReportePatenteQuery,
            ResponseWrapper<ReportePatenteDto>>
    {
        /// <summary>
        /// Obtiene un reporte de patente
        /// mediante su identificador.
        /// </summary>
        public async Task<ResponseWrapper<ReportePatenteDto>> Handle(
            GetReportePatenteQuery request,
            CancellationToken cancellationToken)
        {
            // Busca la patente mediante su identificador.
            var reporte =
                await reportePatenteRepository
                    .GetByIdAsync(
                        request.Id,
                        cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReportePatenteDto>(
                    "No existe el reporte de patente solicitado.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene los inventores asociados a la patente.
            var inventores =
                await inventorPatenteRepository
                    .GetByIdPatenteAsync(
                        reporte.Id,
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
                "Reporte de patente obtenido correctamente");
        }
    }
}