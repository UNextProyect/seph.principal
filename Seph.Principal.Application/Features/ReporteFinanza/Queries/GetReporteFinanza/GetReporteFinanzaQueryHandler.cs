using System.Linq;
using System.Net;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.ReporteFinanza.Queries.GetReporteFinanza
{
    /// <summary>
    /// Procesa la consulta de un reporte financiero.
    /// </summary>
    public sealed class GetReporteFinanzaQueryHandler(
        IReporteFinanzaRepository reporteFinanzaRepository,
        IProyectoFinanciadoRepository proyectoFinanciadoRepository)
        : IRequestHandler<
            GetReporteFinanzaQuery,
            ResponseWrapper<ReporteFinanzaDto>>
    {
        /// <summary>
        /// Obtiene el reporte financiero correspondiente
        /// a una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReporteFinanzaDto>> Handle(
            GetReporteFinanzaQuery request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte registrado para el periodo institucional seleccionado.
            var reporte =
                await reporteFinanzaRepository
                    .GetByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteFinanzaDto>(
                    "No existe un reporte financiero para este periodo.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene los proyectos asociados al reporte financiero.
            var proyectosFinanciados =
                await proyectoFinanciadoRepository
                    .GetByIdReporteFinanzaAsync(
                        reporte.Id,
                        cancellationToken);

            var dto =
                new ReporteFinanzaDto
                {
                    Id =
                        reporte.Id,

                    IdMapInstitucionPeriodo =
                        reporte.IdMapInstitucionPeriodo,

                    MoneyPresupuestoAnual =
                        reporte.MoneyPresupuestoAnual,

                    MoneySubsidioEstatal =
                        reporte.MoneySubsidioEstatal,

                    MoneySubsidioFederal =
                        reporte.MoneySubsidioFederal,

                    MoneyIngresosPropios =
                        reporte.MoneyIngresosPropios,

                    MoneyGastoEjercido =
                        reporte.MoneyGastoEjercido,

                    MoneyGastoAlumno =
                        reporte.MoneyGastoAlumno,

                    BitAdeudos =
                        reporte.BitAdeudos,

                    MoneyMontoAdeudo =
                        reporte.MoneyMontoAdeudo,

                    ProyectosFinanciados =
                        proyectosFinanciados
                            .Select(
                                proyecto =>
                                    new ProyectoFinanciadoDto
                                    {
                                        Id =
                                            proyecto.Id,

                                        StrNombre =
                                            proyecto.StrNombre,

                                        StrOrigenFinanciamiento =
                                            proyecto.StrOrigenFinanciamiento,

                                        StrObjetivo =
                                            proyecto.StrObjetivo
                                    })
                            .ToList()
                };

            return ResponseFactory.Success(
                dto,
                "Reporte financiero obtenido correctamente");
        }
    }
}