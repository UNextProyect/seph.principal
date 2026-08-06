using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System.Linq;
using System.Net;

namespace Seph.Principal.Application.Features.ReporteFinanza.Commands
{
    /// <summary>
    /// Procesa la actualización de un reporte financiero.
    /// </summary>
    public sealed class UpdateReporteFinanzaCommandHandler(
        IReporteFinanzaRepository reporteFinanzaRepository,
        IProyectoFinanciadoRepository proyectoFinanciadoRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            UpdateReporteFinanzaCommand,
            ResponseWrapper<ReporteFinanzaDto>>
    {
        /// <summary>
        /// Actualiza la información capturable
        /// de un reporte financiero.
        /// </summary>
        public async Task<ResponseWrapper<ReporteFinanzaDto>> Handle(
            UpdateReporteFinanzaCommand request,
            CancellationToken cancellationToken)
        {
            // Busca el reporte asociado a la institución y periodo.
            var reporte =
                await reporteFinanzaRepository
                    .GetByMapInstitucionPeriodoForUpdateAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (reporte is null)
            {
                return ResponseFactory.Failure<ReporteFinanzaDto>(
                    "No existe un reporte financiero para actualizar.",
                    HttpStatusCode.NotFound);
            }

            // Actualiza únicamente la información capturable.
            reporte.MoneyPresupuestoAnual =
                request.MoneyPresupuestoAnual;

            reporte.MoneySubsidioEstatal =
                request.MoneySubsidioEstatal;

            reporte.MoneySubsidioFederal =
                request.MoneySubsidioFederal;

            reporte.MoneyIngresosPropios =
                request.MoneyIngresosPropios;

            reporte.MoneyGastoEjercido =
                request.MoneyGastoEjercido;

            reporte.MoneyGastoAlumno =
                request.MoneyGastoAlumno;

            reporte.BitAdeudos =
                request.BitAdeudos;

            reporte.MoneyMontoAdeudo =
                request.BitAdeudos
                    ? request.MoneyMontoAdeudo
                    : 0;

            // Elimina los proyectos registrados anteriormente.
            await proyectoFinanciadoRepository
                .DeleteByIdReporteFinanzaAsync(
                    reporte.Id,
                    cancellationToken);

            // Prepara nuevamente los proyectos enviados en la solicitud.
            var proyectosFinanciados =
                request.ProyectosFinanciados
                    .Select(
                        proyecto =>
                            new ProyectoFinanciado
                            {
                                IdReporteFinanza =
                                    reporte.Id,

                                StrNombre =
                                    proyecto.StrNombre,

                                StrOrigenFinanciamiento =
                                    proyecto.StrOrigenFinanciamiento,

                                StrObjetivo =
                                    proyecto.StrObjetivo
                            })
                    .ToList();

            await proyectoFinanciadoRepository.AddRangeAsync(
                proyectosFinanciados,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            await bitacoraService.RegistrarAsync(
                "Finanza",
                reporte.Id.ToString(),
                "Editar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporte,
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
                "Reporte financiero actualizado correctamente");
        }
    }
}