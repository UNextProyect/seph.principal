using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteInfraestructura.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteInfraestructura
    .Queries.GetReporteInfraestructuraComparativo
{
    /// <summary>
    /// Compara los indicadores de infraestructura
    /// correspondientes a dos periodos seleccionados.
    /// </summary>
    public sealed class GetReporteInfraestructuraComparativoQueryHandler(
        IReporteInfraestructuraRepository reporteInfraestructuraRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteInfraestructuraComparativoQuery,
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteInfraestructuraComparativoDto>>>
    {
        public async Task<
            ResponseWrapper<
                IReadOnlyCollection<
                    ReporteInfraestructuraComparativoDto>>> Handle(
                GetReporteInfraestructuraComparativoQuery request,
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
                        ReporteInfraestructuraComparativoDto>>(
                    "Selecciona dos periodos diferentes.",
                    HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene las dos relaciones
             * institución-periodo seleccionadas.
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
                        ReporteInfraestructuraComparativoDto>>(
                    "No se encontró uno de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Solamente pueden compararse periodos
             * correspondientes a la misma institución.
             */
            if (
                mapPeriodoBase.IdInstitucion !=
                mapPeriodoComparacion.IdInstitucion
            )
            {
                return ResponseFactory.Failure<
                    IReadOnlyCollection<
                        ReporteInfraestructuraComparativoDto>>(
                    "Los periodos seleccionados no pertenecen " +
                    "a la misma institución.",
                    HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene los reportes de infraestructura
             * de los dos periodos.
             */
            var reporteBase =
                await reporteInfraestructuraRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            var reporteComparacion =
                await reporteInfraestructuraRepository
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
                        ReporteInfraestructuraComparativoDto>>(
                    "Uno de los periodos no tiene un reporte " +
                    "de Infraestructura registrado.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Obtiene los nombres de los periodos
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
                        ReporteInfraestructuraComparativoDto>>(
                    "No se encontró la información de uno " +
                    "de los periodos seleccionados.",
                    HttpStatusCode.NotFound);
            }

            /*
             * Genera un comparativo independiente
             * para cada indicador de infraestructura.
             */
            IReadOnlyCollection<
                ReporteInfraestructuraComparativoDto> comparativos =
                new List<ReporteInfraestructuraComparativoDto>
                {
                    CrearComparativo(
                        "Aulas",
                        periodoBase.StrValor,
                        reporteBase.IntTotalAulas,
                        periodoComparacion.StrValor,
                        reporteComparacion.IntTotalAulas),

                    CrearComparativo(
                        "Laboratorios",
                        periodoBase.StrValor,
                        reporteBase.IntTotalLaboratorios,
                        periodoComparacion.StrValor,
                        reporteComparacion.IntTotalLaboratorios),

                    CrearComparativo(
                        "Talleres",
                        periodoBase.StrValor,
                        reporteBase.IntTotalTalleres,
                        periodoComparacion.StrValor,
                        reporteComparacion.IntTotalTalleres),

                    CrearComparativo(
                        "Bibliotecas",
                        periodoBase.StrValor,
                        reporteBase.IntTotalBibliotecas,
                        periodoComparacion.StrValor,
                        reporteComparacion.IntTotalBibliotecas),

                    CrearComparativo(
                        "Equipos de cómputo",
                        periodoBase.StrValor,
                        reporteBase.IntTotalComputo,
                        periodoComparacion.StrValor,
                        reporteComparacion.IntTotalComputo)
                };

            return ResponseFactory.Success(
                comparativos,
                "Comparativo de Infraestructura obtenido correctamente.");
        }

        /// <summary>
        /// Calcula la diferencia, el porcentaje
        /// y el estado de un indicador.
        /// </summary>
        private static ReporteInfraestructuraComparativoDto
            CrearComparativo(
                string indicador,
                string periodoBase,
                int valorBase,
                string periodoComparacion,
                int valorComparacion)
        {
            var diferencia =
                valorBase - valorComparacion;

            var porcentaje =
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

            return new ReporteInfraestructuraComparativoDto(
                indicador,
                periodoBase,
                valorBase,
                periodoComparacion,
                valorComparacion,
                diferencia,
                porcentaje,
                estado);
        }
    }
}