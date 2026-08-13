using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteFinanza
    .Queries.GetReporteFinanzaComparativo
{
    /// <summary>
    /// Compara los indicadores financieros
    /// correspondientes a dos periodos seleccionados.
    /// </summary>
    public sealed class GetReporteFinanzaComparativoQueryHandler(
        IReporteFinanzaRepository reporteFinanzaRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteFinanzaComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteFinanzaComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteFinanzaComparativoDto>>> Handle(
                GetReporteFinanzaComparativoQuery request,
                CancellationToken cancellationToken)
        {
            /*
             * Evita comparar dos veces la misma
             * relación institución-periodo.
             */
            if (
                request.IdMapPeriodoBase ==
                request.IdMapPeriodoComparacion
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReporteFinanzaComparativoDto>>(
                    "Selecciona dos periodos diferentes.",
                    HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene las relaciones institución-periodo
             * seleccionadas para la comparación.
             */
            var mapPeriodoBase =
                await mapInstitucionPeriodoRepository.GetByIdAsync(
                    request.IdMapPeriodoBase,
                    cancellationToken);

            var mapPeriodoComparacion =
                await mapInstitucionPeriodoRepository.GetByIdAsync(
                    request.IdMapPeriodoComparacion,
                    cancellationToken);

            if (
                mapPeriodoBase is null ||
                mapPeriodoComparacion is null
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReporteFinanzaComparativoDto>>(
                    "No se encontró uno de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Ambos periodos deben pertenecer
             * a la misma institución.
             */
            if (
                mapPeriodoBase.IdInstitucion !=
                mapPeriodoComparacion.IdInstitucion
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReporteFinanzaComparativoDto>>(
                    "Los periodos seleccionados no pertenecen " +
                    "a la misma institución.",
                    HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene los reportes financieros
             * correspondientes a los dos periodos.
             */
            var reporteBase =
                await reporteFinanzaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            var reporteComparacion =
                await reporteFinanzaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoComparacion,
                        cancellationToken);

            if (
                reporteBase is null ||
                reporteComparacion is null
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReporteFinanzaComparativoDto>>(
                    "Uno de los periodos no tiene un reporte " +
                    "de Finanzas registrado.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Consulta los nombres de los periodos
             * que se incluirán en cada indicador.
             */
            var periodoBase =
                await catPeriodoRepository.GetByIdAsync(
                    mapPeriodoBase.IdPeriodo,
                    cancellationToken);

            var periodoComparacion =
                await catPeriodoRepository.GetByIdAsync(
                    mapPeriodoComparacion.IdPeriodo,
                    cancellationToken);

            if (
                periodoBase is null ||
                periodoComparacion is null
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReporteFinanzaComparativoDto>>(
                    "No se encontró la información de uno " +
                    "de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Genera un comparativo para cada
             * indicador financiero registrado.
             */
            IReadOnlyCollection<
                ReporteFinanzaComparativoDto> comparativos =
                new List<ReporteFinanzaComparativoDto>
                {
                    CrearComparativo(
                        "Presupuesto anual",
                        periodoBase.StrValor,
                        reporteBase.MoneyPresupuestoAnual,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneyPresupuestoAnual),

                    CrearComparativo(
                        "Subsidio estatal",
                        periodoBase.StrValor,
                        reporteBase.MoneySubsidioEstatal,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneySubsidioEstatal),

                    CrearComparativo(
                        "Subsidio federal",
                        periodoBase.StrValor,
                        reporteBase.MoneySubsidioFederal,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneySubsidioFederal),

                    CrearComparativo(
                        "Ingresos propios",
                        periodoBase.StrValor,
                        reporteBase.MoneyIngresosPropios,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneyIngresosPropios),

                    CrearComparativo(
                        "Gasto ejercido",
                        periodoBase.StrValor,
                        reporteBase.MoneyGastoEjercido,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneyGastoEjercido),

                    CrearComparativo(
                        "Gasto por alumno",
                        periodoBase.StrValor,
                        reporteBase.MoneyGastoAlumno,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneyGastoAlumno),

                    CrearComparativo(
                        "Monto de adeudos",
                        periodoBase.StrValor,
                        reporteBase.MoneyMontoAdeudo,
                        periodoComparacion.StrValor,
                        reporteComparacion.MoneyMontoAdeudo)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de Finanzas obtenido correctamente.");
        }

        /// <summary>
        /// Calcula la diferencia, el cambio porcentual
        /// y el estado de un indicador financiero.
        /// </summary>
        private static ReporteFinanzaComparativoDto
            CrearComparativo(
                string indicador,
                string periodoBase,
                decimal valorBase,
                string periodoComparacion,
                decimal valorComparacion)
        {
            var diferencia =
                valorBase - valorComparacion;

            var porcentajeCambio =
                valorComparacion == 0
                    ? 0
                    : Math.Round(
                        diferencia /
                        valorComparacion *
                        100,
                        2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            return new ReporteFinanzaComparativoDto(
                indicador,
                periodoBase,
                valorBase,
                periodoComparacion,
                valorComparacion,
                diferencia,
                porcentajeCambio,
                estado);
        }
    }
}