using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaComparativo
{
    /// <summary>
    /// Obtiene el comparativo de los indicadores financieros
    /// entre el periodo actual y el periodo anterior.
    /// </summary>
    public sealed class GetReporteFinanzaComparativoQueryHandler(
        IReporteFinanzaRepository reporteFinanzaRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteFinanzaComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<ReporteFinanzaComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<ReporteFinanzaComparativoDto>>>
            Handle(
                GetReporteFinanzaComparativoQuery request,
                CancellationToken cancellationToken)
        {
            // Obtiene el reporte financiero del periodo actual.
            var reporteActual =
                await reporteFinanzaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (reporteActual is null)
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<ReporteFinanzaComparativoDto>>(
                    "No existe un reporte financiero para este periodo.",
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
                    IReadOnlyCollection<ReporteFinanzaComparativoDto>>(
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
                    IReadOnlyCollection<ReporteFinanzaComparativoDto>>(
                    "No existe el periodo actual.",
                    HttpStatusCode.NotFound);
            }

            // Busca el reporte anterior de la misma institución.
            var reporteAnterior =
                await reporteFinanzaRepository
                    .GetPreviousReporteAsync(
                        mapActual.IdInstitucion,
                        periodoActual.IntAnio,
                        periodoActual.IntNumeroPeriodo,
                        cancellationToken);

            if (reporteAnterior is null)
            {
                IReadOnlyCollection<ReporteFinanzaComparativoDto>
                    comparativosSinAnterior =
                    CrearComparativosSinPeriodoAnterior(
                        periodoActual.StrValor,
                        reporteActual);

                return ResponseFactory.Success(
                    comparativosSinAnterior,
                    "No existe un periodo anterior para comparar.");
            }

            var mapAnterior =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        reporteAnterior.IdMapInstitucionPeriodo,
                        cancellationToken);

            var periodoAnterior = mapAnterior is null
                ? null
                : await catPeriodoRepository.GetByIdAsync(
                    mapAnterior.IdPeriodo,
                    cancellationToken);

            IReadOnlyCollection<ReporteFinanzaComparativoDto>
                comparativos =
                    new List<ReporteFinanzaComparativoDto>
                    {
                        CrearComparativo(
                            "Presupuesto anual",
                            periodoActual.StrValor,
                            reporteActual.MoneyPresupuestoAnual,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneyPresupuestoAnual),

                        CrearComparativo(
                            "Subsidio estatal",
                            periodoActual.StrValor,
                            reporteActual.MoneySubsidioEstatal,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneySubsidioEstatal),

                        CrearComparativo(
                            "Subsidio federal",
                            periodoActual.StrValor,
                            reporteActual.MoneySubsidioFederal,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneySubsidioFederal),

                        CrearComparativo(
                            "Ingresos propios",
                            periodoActual.StrValor,
                            reporteActual.MoneyIngresosPropios,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneyIngresosPropios),

                        CrearComparativo(
                            "Gasto ejercido",
                            periodoActual.StrValor,
                            reporteActual.MoneyGastoEjercido,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneyGastoEjercido),

                        CrearComparativo(
                            "Gasto por alumno",
                            periodoActual.StrValor,
                            reporteActual.MoneyGastoAlumno,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneyGastoAlumno),

                        CrearComparativo(
                            "Monto de adeudos",
                            periodoActual.StrValor,
                            reporteActual.MoneyMontoAdeudo,
                            periodoAnterior?.StrValor,
                            reporteAnterior.MoneyMontoAdeudo)
                    };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo financiero obtenido correctamente");
        }

        /// <summary>
        /// Construye el comparativo de un indicador financiero.
        /// </summary>
        private static ReporteFinanzaComparativoDto CrearComparativo(
            string indicador,
            string periodoActual,
            decimal valorActual,
            string? periodoAnterior,
            decimal valorAnterior)
        {
            var diferencia = valorActual - valorAnterior;

            var porcentaje = valorAnterior == 0
                ? 0
                : Math.Round(
                    diferencia /
                    valorAnterior *
                    100,
                    2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            return new ReporteFinanzaComparativoDto(
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
        /// Construye los indicadores cuando no existe
        /// un reporte correspondiente al periodo anterior.
        /// </summary>
        private static IReadOnlyCollection<
            ReporteFinanzaComparativoDto>
            CrearComparativosSinPeriodoAnterior(
                string periodoActual,
                Domain.Entities.ReporteFinanza reporteActual)
        {
            return new List<ReporteFinanzaComparativoDto>
            {
                CrearSinPeriodoAnterior(
                    "Presupuesto anual",
                    periodoActual,
                    reporteActual.MoneyPresupuestoAnual),

                CrearSinPeriodoAnterior(
                    "Subsidio estatal",
                    periodoActual,
                    reporteActual.MoneySubsidioEstatal),

                CrearSinPeriodoAnterior(
                    "Subsidio federal",
                    periodoActual,
                    reporteActual.MoneySubsidioFederal),

                CrearSinPeriodoAnterior(
                    "Ingresos propios",
                    periodoActual,
                    reporteActual.MoneyIngresosPropios),

                CrearSinPeriodoAnterior(
                    "Gasto ejercido",
                    periodoActual,
                    reporteActual.MoneyGastoEjercido),

                CrearSinPeriodoAnterior(
                    "Gasto por alumno",
                    periodoActual,
                    reporteActual.MoneyGastoAlumno),

                CrearSinPeriodoAnterior(
                    "Monto de adeudos",
                    periodoActual,
                    reporteActual.MoneyMontoAdeudo)
            };
        }

        /// <summary>
        /// Construye un indicador cuando no existe un periodo anterior.
        /// </summary>
        private static ReporteFinanzaComparativoDto
            CrearSinPeriodoAnterior(
                string indicador,
                string periodoActual,
                decimal valorActual)
        {
            return new ReporteFinanzaComparativoDto(
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