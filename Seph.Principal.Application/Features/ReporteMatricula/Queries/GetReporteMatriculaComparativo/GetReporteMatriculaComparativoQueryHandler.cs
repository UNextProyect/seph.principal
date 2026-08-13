using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteMatricula.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteMatricula.Queries.GetReporteMatriculaComparativo
{
    /// <summary>
    /// Compara la matrícula registrada
    /// entre dos periodos seleccionados.
    /// </summary>
    public sealed class GetReporteMatriculaComparativoQueryHandler(
        IReporteMatriculaRepository reporteMatriculaRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReporteMatriculaComparativoQuery,
            ResponseWrapper<ReporteMatriculaComparativoDto>>
    {
        public async Task<
            ResponseWrapper<ReporteMatriculaComparativoDto>>
            Handle(
                GetReporteMatriculaComparativoQuery request,
                CancellationToken cancellationToken)
        {
            /*
             * Evita comparar el mismo periodo
             * en ambos selectores.
             */
            if (
                request.IdMapPeriodoBase ==
                request.IdMapPeriodoComparacion
            )
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "Los periodos seleccionados deben ser diferentes.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene las relaciones institución-periodo
             * correspondientes a la comparación.
             */
            var mapPeriodoBase =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            if (mapPeriodoBase is null)
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "No existe el periodo base seleccionado.",
                        HttpStatusCode.NotFound);
            }

            var mapPeriodoComparacion =
                await mapInstitucionPeriodoRepository
                    .GetByIdAsync(
                        request.IdMapPeriodoComparacion,
                        cancellationToken);

            if (mapPeriodoComparacion is null)
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "No existe el periodo seleccionado para comparar.",
                        HttpStatusCode.NotFound);
            }

            /*
             * Los dos periodos deben pertenecer
             * a la misma institución.
             */
            if (
                mapPeriodoBase.IdInstitucion !=
                mapPeriodoComparacion.IdInstitucion
            )
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "Los periodos seleccionados no pertenecen a la misma institución.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene los reportes de matrícula
             * correspondientes a ambos periodos.
             */
            var reporteBase =
                await reporteMatriculaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            if (reporteBase is null)
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "No existe un reporte de matrícula para el periodo base.",
                        HttpStatusCode.NotFound);
            }

            var reporteComparacion =
                await reporteMatriculaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoComparacion,
                        cancellationToken);

            if (reporteComparacion is null)
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "No existe un reporte de matrícula para el periodo de comparación.",
                        HttpStatusCode.NotFound);
            }

            /*
             * Obtiene los nombres de los periodos
             * que se mostrarán en el resultado.
             */
            var periodoBase =
                await catPeriodoRepository.GetByIdAsync(
                    mapPeriodoBase.IdPeriodo,
                    cancellationToken);

            if (periodoBase is null)
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "No se encontró la información del periodo base.",
                        HttpStatusCode.NotFound);
            }

            var periodoComparacion =
                await catPeriodoRepository.GetByIdAsync(
                    mapPeriodoComparacion.IdPeriodo,
                    cancellationToken);

            if (periodoComparacion is null)
            {
                return ResponseFactory
                    .Failure<ReporteMatriculaComparativoDto>(
                        "No se encontró la información del periodo de comparación.",
                        HttpStatusCode.NotFound);
            }

            /*
             * Calcula la diferencia tomando
             * el periodo base como referencia.
             */
            var diferencia =
                reporteBase.IntTotal -
                reporteComparacion.IntTotal;

            var porcentajeCambio =
                reporteComparacion.IntTotal == 0
                    ? 0
                    : Math.Round(
                        (decimal)diferencia /
                        reporteComparacion.IntTotal *
                        100,
                        2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            var comparativo =
                new ReporteMatriculaComparativoDto(
                    periodoBase.StrValor,
                    reporteBase.IntTotal,
                    periodoComparacion.StrValor,
                    reporteComparacion.IntTotal,
                    diferencia,
                    porcentajeCambio,
                    estado);

            return ResponseFactory.Success(
                comparativo,
                "Comparativo de matrícula obtenido correctamente.");
        }
    }
}
