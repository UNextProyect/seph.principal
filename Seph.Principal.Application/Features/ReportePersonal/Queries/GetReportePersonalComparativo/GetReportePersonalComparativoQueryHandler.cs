using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePersonal.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReportePersonal.Queries.GetReportePersonalComparativo
{
    public sealed class GetReportePersonalComparativoQueryHandler(
        IReportePersonalRepository reportePersonalRepository,
        IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
        ICatPeriodoRepository catPeriodoRepository)
        : IRequestHandler<
            GetReportePersonalComparativoQuery,
            ResponseWrapper<ReportePersonalComparativoDto>>
    {
        public async Task<
            ResponseWrapper<ReportePersonalComparativoDto>> Handle(
            GetReportePersonalComparativoQuery request,
            CancellationToken cancellationToken)
        {
            /*
             * Evita comparar dos veces la misma
             * relación institución-periodo.
             */
            if (request.IdMapPeriodoBase ==
                request.IdMapPeriodoComparacion)
            {
                return ResponseFactory
                    .Failure<ReportePersonalComparativoDto>(
                        "Selecciona dos periodos diferentes.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene las relaciones institución-periodo
             * seleccionadas por el usuario.
             */
            var mapPeriodoBase =
                await mapInstitucionPeriodoRepository.GetByIdAsync(
                    request.IdMapPeriodoBase,
                    cancellationToken);

            var mapPeriodoComparacion =
                await mapInstitucionPeriodoRepository.GetByIdAsync(
                    request.IdMapPeriodoComparacion,
                    cancellationToken);

            if (mapPeriodoBase is null ||
                mapPeriodoComparacion is null)
            {
                return ResponseFactory
                    .Failure<ReportePersonalComparativoDto>(
                        "No se encontró uno de los periodos seleccionados.",
                        HttpStatusCode.NotFound);
            }

            /*
             * La comparación solamente es válida cuando
             * ambos periodos pertenecen a la misma institución.
             */
            if (mapPeriodoBase.IdInstitucion !=
                mapPeriodoComparacion.IdInstitucion)
            {
                return ResponseFactory
                    .Failure<ReportePersonalComparativoDto>(
                        "Los periodos seleccionados no pertenecen " +
                        "a la misma institución.",
                        HttpStatusCode.BadRequest);
            }

            /*
             * Obtiene los reportes de Personal
             * correspondientes a ambos periodos.
             */
            var reporteBase =
                await reportePersonalRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoBase,
                        cancellationToken);

            var reporteComparacion =
                await reportePersonalRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapPeriodoComparacion,
                        cancellationToken);

            if (reporteBase is null ||
                reporteComparacion is null)
            {
                return ResponseFactory
                    .Failure<ReportePersonalComparativoDto>(
                        "Uno de los periodos no tiene un reporte " +
                        "de Personal registrado.",
                        HttpStatusCode.NotFound);
            }

            /*
             * Consulta los nombres de los periodos
             * para incluirlos en el resultado.
             */
            var periodoBase =
                await catPeriodoRepository.GetByIdAsync(
                    mapPeriodoBase.IdPeriodo,
                    cancellationToken);

            var periodoComparacion =
                await catPeriodoRepository.GetByIdAsync(
                    mapPeriodoComparacion.IdPeriodo,
                    cancellationToken);

            if (periodoBase is null ||
                periodoComparacion is null)
            {
                return ResponseFactory
                    .Failure<ReportePersonalComparativoDto>(
                        "No se encontró la información de uno " +
                        "de los periodos seleccionados.",
                        HttpStatusCode.NotFound);
            }

            /*
             * La diferencia representa el total del periodo base
             * menos el total del periodo de comparación.
             */
            var diferencia =
                reporteBase.IntTotalGeneral -
                reporteComparacion.IntTotalGeneral;

            /*
             * Evita una división entre cero cuando el periodo
             * de comparación no tiene personal registrado.
             */
            var porcentaje =
                reporteComparacion.IntTotalGeneral == 0
                    ? 0
                    : Math.Round(
                        (decimal)diferencia /
                        reporteComparacion.IntTotalGeneral *
                        100,
                        2);

            var estado = diferencia > 0
                ? "Aumentó"
                : diferencia < 0
                    ? "Disminuyó"
                    : "Sin cambios";

            var comparativo =
                new ReportePersonalComparativoDto(
                    periodoBase.StrValor,
                    reporteBase.IntTotalGeneral,
                    periodoComparacion.StrValor,
                    reporteComparacion.IntTotalGeneral,
                    diferencia,
                    porcentaje,
                    estado);

            return ResponseFactory.Success(
                comparativo,
                "Comparativo de Personal obtenido correctamente.");
        }
    }
}