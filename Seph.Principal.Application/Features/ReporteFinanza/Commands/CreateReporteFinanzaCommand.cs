using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using System;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReporteFinanza.Commands
{
    /// <summary>
    /// Comando para registrar un reporte financiero.
    /// </summary>
    public sealed record CreateReporteFinanzaCommand(
        long IdMapInstitucionPeriodo,
        decimal MoneyPresupuestoAnual,
        decimal MoneySubsidioEstatal,
        decimal MoneySubsidioFederal,
        decimal MoneyIngresosPropios,
        decimal MoneyGastoEjercido,
        decimal MoneyGastoAlumno,
        bool BitAdeudos,
        decimal MoneyMontoAdeudo,
        Guid IdUsuarioRegistro,
        List<ProyectoFinanciadoDto> ProyectosFinanciados)
        : IRequest<ResponseWrapper<ReporteFinanzaDto>>;
}