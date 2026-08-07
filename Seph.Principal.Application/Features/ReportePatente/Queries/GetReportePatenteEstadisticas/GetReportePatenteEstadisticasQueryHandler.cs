using System.Linq;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteEstadisticas
{
    /// <summary>
    /// Obtiene los indicadores de patentes
    /// correspondientes a una institución y periodo.
    /// </summary>
    public sealed class GetReportePatenteEstadisticasQueryHandler(
        IReportePatenteRepository reportePatenteRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReportePatenteEstadisticasQuery,
            ResponseWrapper<ReportePatenteEstadisticasDto>>
    {
        /// <summary>
        /// Procesa la consulta de estadísticas de patentes.
        /// </summary>
        public async Task<
            ResponseWrapper<ReportePatenteEstadisticasDto>>
            Handle(
                GetReportePatenteEstadisticasQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene las patentes capturadas
            // para el periodo institucional.
            var reportes =
                await reportePatenteRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (!reportes.Any())
            {
                return ResponseFactory
                    .Failure<ReportePatenteEstadisticasDto>(
                        "No existen reportes de patentes para este periodo.",
                        HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo.
            var mapInstitucionPeriodo =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (mapInstitucionPeriodo is null)
            {
                return ResponseFactory
                    .Failure<ReportePatenteEstadisticasDto>(
                        "No existe la relación institución-periodo.",
                        HttpStatusCode.NotFound);
            }

            // Obtiene la información del periodo seleccionado.
            var periodo =
                await catPeriodoRepository
                    .GetByIdAsync(
                        mapInstitucionPeriodo.IdPeriodo,
                        cancellationToken);

            if (periodo is null)
            {
                return ResponseFactory
                    .Failure<ReportePatenteEstadisticasDto>(
                        "No existe el periodo seleccionado.",
                        HttpStatusCode.NotFound);
            }

            var dto =
                new ReportePatenteEstadisticasDto(
                    periodo.StrValor,
                    reportes.Count);

            return ResponseFactory.Success(
                dto,
                "Estadísticas de patentes obtenidas correctamente");
        }
    }
}