using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReporteFinanza.DTOs;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReporteFinanza.Commands
{
    /*
     * Actualiza el reporte financiero asociado
     * a una relación institución-periodo.
     */
    public sealed record UpdateReporteFinanzaCommand(
        long IdMapInstitucionPeriodo,
        decimal MoneyPresupuestoAnual,
        decimal MoneySubsidioEstatal,
        decimal MoneySubsidioFederal,
        decimal MoneyIngresosPropios,
        decimal MoneyGastoEjercido,
        decimal MoneyGastoAlumno,
        bool BitAdeudos,
        decimal MoneyMontoAdeudo,
        List<ProyectoFinanciadoDto> ProyectosFinanciados)
        : IRequest<ResponseWrapper<ReporteFinanzaDto>>;
}