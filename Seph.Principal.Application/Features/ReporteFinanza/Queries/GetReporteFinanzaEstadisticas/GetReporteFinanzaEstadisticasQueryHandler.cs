using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaEstadisticas
{
    /// <summary>
    /// Obtiene los indicadores financieros
    /// correspondientes a una institución y periodo.
    /// </summary>
    public sealed class GetReporteFinanzaEstadisticasQueryHandler(
        IReporteFinanzaRepository reporteFinanzaRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteFinanzaEstadisticasQuery,
            ResponseWrapper<ReporteFinanzaEstadisticasDto>>
    {
        /// <summary>
        /// Procesa la consulta de estadísticas financieras.
        /// </summary>
        public async Task<ResponseWrapper<ReporteFinanzaEstadisticasDto>>
            Handle(
                GetReporteFinanzaEstadisticasQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene el reporte capturado para el periodo institucional.
            var reporte =
                await reporteFinanzaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory
                    .Failure<ReporteFinanzaEstadisticasDto>(
                        "No existe un reporte financiero para este periodo.",
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
                    .Failure<ReporteFinanzaEstadisticasDto>(
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
                    .Failure<ReporteFinanzaEstadisticasDto>(
                        "No existe el periodo seleccionado.",
                        HttpStatusCode.NotFound);
            }

            var dto =
                new ReporteFinanzaEstadisticasDto(
                    periodo.StrValor,
                    reporte.MoneyPresupuestoAnual,
                    reporte.MoneySubsidioEstatal,
                    reporte.MoneySubsidioFederal,
                    reporte.MoneyIngresosPropios,
                    reporte.MoneyGastoEjercido,
                    reporte.MoneyGastoAlumno,
                    reporte.MoneyMontoAdeudo);

            return ResponseFactory.Success(
                dto,
                "Estadísticas financieras obtenidas correctamente");
        }
    }
}