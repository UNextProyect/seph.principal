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
    /// Compara el total de patentes registradas
    /// en dos periodos seleccionados.
    /// </summary>
    public sealed class GetReportePatenteComparativoQueryHandler(
        IReportePatenteRepository reportePatenteRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReportePatenteComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<
                    ReportePatenteComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReportePatenteComparativoDto>>> Handle(
                GetReportePatenteComparativoQuery request,
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
                        ReportePatenteComparativoDto>>(
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
                        ReportePatenteComparativoDto>>(
                    "No se encontró uno de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Los periodos deben pertenecer
             * a la misma institución.
             */
            if (
                mapPeriodoBase.IdInstitucion !=
                mapPeriodoComparacion.IdInstitucion
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReportePatenteComparativoDto>>(
                    "Los periodos seleccionados no pertenecen " +
                    "a la misma institución.",
                    HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene todas las patentes registradas
             * dentro de cada periodo seleccionado.
             */
            var reportesBase =
                await reportePatenteRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            var reportesComparacion =
                await reportePatenteRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoComparacion,
                        cancellationToken);

            if (
                !reportesBase.Any() ||
                !reportesComparacion.Any()
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReportePatenteComparativoDto>>(
                    "Uno de los periodos no tiene patentes registradas.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Consulta los nombres de los periodos
             * que se mostrarán en el frontend.
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
                        ReportePatenteComparativoDto>>(
                    "No se encontró la información de uno " +
                    "de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Cuenta las patentes registradas
             * dentro de cada periodo.
             */
            var totalBase =
                reportesBase.Count;

            var totalComparacion =
                reportesComparacion.Count;

            IReadOnlyCollection<
                ReportePatenteComparativoDto> comparativos =
                new List<ReportePatenteComparativoDto>
                {
                    CrearComparativo(
                        "Patentes registradas",
                        periodoBase.StrValor,
                        totalBase,
                        periodoComparacion.StrValor,
                        totalComparacion)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de Patentes obtenido correctamente.");
        }

        /// <summary>
        /// Calcula la diferencia, el cambio porcentual
        /// y el estado del total de patentes.
        /// </summary>
        private static ReportePatenteComparativoDto
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

            return new ReportePatenteComparativoDto(
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