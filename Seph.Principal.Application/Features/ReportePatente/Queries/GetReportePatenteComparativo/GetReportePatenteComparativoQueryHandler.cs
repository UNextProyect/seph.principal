using System.Linq;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePatente
    .Queries.GetReportePatenteComparativo
{
    /// <summary>
    /// Obtiene el comparativo del total de patentes
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed class GetReportePatenteComparativoQueryHandler(
        IReportePatenteRepository reportePatenteRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReportePatenteComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<ReportePatenteComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<ReportePatenteComparativoDto>>>
            Handle(
                GetReportePatenteComparativoQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene las patentes registradas
            // en el periodo actual.
            var reportesActuales =
                await reportePatenteRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (!reportesActuales.Any())
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReportePatenteComparativoDto>>(
                    "No existen reportes de patentes para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la relación institución-periodo actual.
            var mapActual =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (mapActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReportePatenteComparativoDto>>(
                    "No existe la relación institución-periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene la información del periodo actual.
            var periodoActual =
                await catPeriodoRepository
                    .GetByIdAsync(
                        mapActual.IdPeriodo,
                        cancellationToken);

            if (periodoActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReportePatenteComparativoDto>>(
                    "No existe el periodo actual.",
                    HttpStatusCode.NotFound);
            }

            // Busca las patentes del periodo anterior
            // pertenecientes a la misma institución.
            var reportesAnteriores =
                await reportePatenteRepository
                    .GetPreviousReportesAsync(
                        mapActual.IdInstitucion,
                        periodoActual.IntAnio,
                        periodoActual.IntNumeroPeriodo,
                        cancellationToken);

            var totalActual =
                reportesActuales.Count;

            if (!reportesAnteriores.Any())
            {
                IReadOnlyCollection<ReportePatenteComparativoDto>
                    comparativosSinAnterior =
                    CrearComparativosSinPeriodoAnterior(
                        periodoActual.StrValor,
                        totalActual);

                return ResponseFactory.Success(
                    comparativosSinAnterior,
                    "No existe un periodo anterior para comparar.");
            }

            var primerReporteAnterior =
                reportesAnteriores.First();

            var mapAnterior =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        primerReporteAnterior.IdMapInstitucionPeriodo,
                        cancellationToken);

            var periodoAnterior =
                mapAnterior is null
                    ? null
                    : await catPeriodoRepository.GetByIdAsync(
                        mapAnterior.IdPeriodo,
                        cancellationToken);

            var totalAnterior =
                reportesAnteriores.Count;

            IReadOnlyCollection<ReportePatenteComparativoDto>
                comparativos =
                new List<ReportePatenteComparativoDto>
                {
                    CrearComparativo(
                        "Patentes registradas",
                        periodoActual.StrValor,
                        totalActual,
                        periodoAnterior?.StrValor,
                        totalAnterior)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de patentes obtenido correctamente");
        }

        /// <summary>
        /// Construye el comparativo del total de patentes.
        /// </summary>
        private static ReportePatenteComparativoDto CrearComparativo(
            string indicador,
            string periodoActual,
            int valorActual,
            string? periodoAnterior,
            int valorAnterior)
        {
            var diferencia =
                valorActual - valorAnterior;

            var porcentaje =
                valorAnterior == 0
                    ? 0
                    : Math.Round(
                        (decimal)diferencia /
                        valorAnterior *
                        100,
                        2);

            var estado =
                diferencia > 0
                    ? "Aumentó"
                    : diferencia < 0
                        ? "Disminuyó"
                        : "Sin cambios";

            return new ReportePatenteComparativoDto(
                indicador,
                periodoActual,
                valorActual,
                periodoAnterior,
                valorAnterior,
                diferencia,
                porcentaje,
                estado);
        }

        /// <summary>
        /// Construye el comparativo cuando no existen
        /// patentes correspondientes al periodo anterior.
        /// </summary>
        private static IReadOnlyCollection<
            ReportePatenteComparativoDto>
            CrearComparativosSinPeriodoAnterior(
                string periodoActual,
                int totalActual)
        {
            return new List<ReportePatenteComparativoDto>
            {
                CrearSinPeriodoAnterior(
                    "Patentes registradas",
                    periodoActual,
                    totalActual)
            };
        }

        /// <summary>
        /// Construye un indicador cuando no existe
        /// información del periodo anterior.
        /// </summary>
        private static ReportePatenteComparativoDto
            CrearSinPeriodoAnterior(
                string indicador,
                string periodoActual,
                int valorActual)
        {
            return new ReportePatenteComparativoDto(
                indicador,
                periodoActual,
                valorActual,
                null,
                null,
                0,
                0,
                "Sin periodo anterior");
        }
    }
}