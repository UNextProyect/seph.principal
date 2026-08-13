using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteVinculacion.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteVinculacion
    .Queries.GetReporteVinculacionComparativo
{
    /// <summary>
    /// Compara los indicadores de vinculación
    /// correspondientes a dos periodos seleccionados.
    /// </summary>
    public sealed class GetReporteVinculacionComparativoQueryHandler(
        IReporteVinculacionRepository reporteVinculacionRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteVinculacionComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteVinculacionComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteVinculacionComparativoDto>>> Handle(
                GetReporteVinculacionComparativoQuery request,
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
                        ReporteVinculacionComparativoDto>>(
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
                        ReporteVinculacionComparativoDto>>(
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
                        ReporteVinculacionComparativoDto>>(
                    "Los periodos seleccionados no pertenecen " +
                    "a la misma institución.",
                    HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene los reportes de Vinculación
             * correspondientes a los dos periodos.
             */
            var reporteBase =
                await reporteVinculacionRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            var reporteComparacion =
                await reporteVinculacionRepository
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
                        ReporteVinculacionComparativoDto>>(
                    "Uno de los periodos no tiene un reporte " +
                    "de Vinculación registrado.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Consulta los nombres de los periodos
             * que se incluirán en el resultado.
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
                        ReporteVinculacionComparativoDto>>(
                    "No se encontró la información de uno " +
                    "de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Actualmente se compara el total
             * de convenios activos.
             */
            IReadOnlyCollection<
                ReporteVinculacionComparativoDto> comparativos =
                new List<ReporteVinculacionComparativoDto>
                {
                    CrearComparativo(
                        "Convenios activos",
                        periodoBase.StrValor,
                        reporteBase.IntTotalConveniosActivos,
                        periodoComparacion.StrValor,
                        reporteComparacion.IntTotalConveniosActivos)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de Vinculación obtenido correctamente.");
        }

        /// <summary>
        /// Calcula la diferencia, el cambio porcentual
        /// y el estado de un indicador de vinculación.
        /// </summary>
        private static ReporteVinculacionComparativoDto
            CrearComparativo(
                string indicador,
                string periodoBase,
                int valorBase,
                string periodoComparacion,
                int valorComparacion)
        {
            var diferencia =
                valorBase - valorComparacion;

            var porcentajeCambio =
                valorComparacion == 0
                    ? 0
                    : Math.Round(
                        (decimal)diferencia /
                        valorComparacion *
                        100,
                        2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            return new ReporteVinculacionComparativoDto(
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