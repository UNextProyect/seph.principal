using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.ReporteFinanza.Commands
{
    /// <summary>
    /// Procesa la creación de un reporte financiero.
    /// </summary>
    public sealed class CreateReporteFinanzaCommandHandler(
        IReporteFinanzaRepository reporteFinanzaRepository,
        IProyectoFinanciadoRepository proyectoFinanciadoRepository,
        IUnitOfWork unitOfWork,
        IBitacoraService bitacoraService,
        ICurrentUserService currentUserService)
        : IRequestHandler<
            CreateReporteFinanzaCommand,
            ResponseWrapper<ReporteFinanzaDto>>
    {
        /// <summary>
        /// Crea un reporte financiero para una institución y periodo.
        /// </summary>
        public async Task<ResponseWrapper<ReporteFinanzaDto>> Handle(
            CreateReporteFinanzaCommand request,
            CancellationToken cancellationToken)
        {
            // Evita registrar dos reportes para el mismo periodo institucional.
            var exists =
                await reporteFinanzaRepository
                    .ExistsByMapInstitucionPeriodoAsync(
                        request.IdMapInstitucionPeriodo,
                        cancellationToken);

            if (exists)
            {
                return ResponseFactory.Failure<ReporteFinanzaDto>(
                    "Ya existe un reporte financiero registrado para este periodo.",
                    HttpStatusCode.BadRequest);
            }

            var reporteFinanza =
                new Domain.Entities.ReporteFinanza
                {
                    IdMapInstitucionPeriodo =
                        request.IdMapInstitucionPeriodo,

                    MoneyPresupuestoAnual =
                        request.MoneyPresupuestoAnual,

                    MoneySubsidioEstatal =
                        request.MoneySubsidioEstatal,

                    MoneySubsidioFederal =
                        request.MoneySubsidioFederal,

                    MoneyIngresosPropios =
                        request.MoneyIngresosPropios,

                    MoneyGastoEjercido =
                        request.MoneyGastoEjercido,

                    MoneyGastoAlumno =
                        request.MoneyGastoAlumno,

                    BitAdeudos =
                        request.BitAdeudos,

                    MoneyMontoAdeudo =
                        request.BitAdeudos
                            ? request.MoneyMontoAdeudo
                            : 0,

                    DateTimeFechaRegistro =
                        DateTime.Now,

                    IdUsuarioRegistro =
                        request.IdUsuarioRegistro,

                    BitActivo =
                        true
                };

            await reporteFinanzaRepository.AddAsync(
                reporteFinanza,
                cancellationToken);

            // Guarda primero el reporte para obtener su identificador.
            await unitOfWork.SaveChangesAsync(
                cancellationToken);

            var proyectosFinanciados =
                request.ProyectosFinanciados
                    .Select(
                        proyecto =>
                            new ProyectoFinanciado
                            {
                                IdReporteFinanza =
                                    reporteFinanza.Id,

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
                reporteFinanza.Id.ToString(),
                "Agregar",
                currentUserService.UserId?.ToString() ?? "desconocido",
                currentUserService.Email?.ToString() ?? "desconocido",
                reporteFinanza,
                cancellationToken);

            var dto =
                new ReporteFinanzaDto
                {
                    Id =
                        reporteFinanza.Id,

                    IdMapInstitucionPeriodo =
                        reporteFinanza.IdMapInstitucionPeriodo,

                    MoneyPresupuestoAnual =
                        reporteFinanza.MoneyPresupuestoAnual,

                    MoneySubsidioEstatal =
                        reporteFinanza.MoneySubsidioEstatal,

                    MoneySubsidioFederal =
                        reporteFinanza.MoneySubsidioFederal,

                    MoneyIngresosPropios =
                        reporteFinanza.MoneyIngresosPropios,

                    MoneyGastoEjercido =
                        reporteFinanza.MoneyGastoEjercido,

                    MoneyGastoAlumno =
                        reporteFinanza.MoneyGastoAlumno,

                    BitAdeudos =
                        reporteFinanza.BitAdeudos,

                    MoneyMontoAdeudo =
                        reporteFinanza.MoneyMontoAdeudo,

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
                "Reporte financiero registrado correctamente");
        }
    }
}