using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using System;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReportePatente.Commands
{
    /// <summary>
    /// Comando para registrar un reporte de patente.
    /// </summary>
    public sealed record CreateReportePatenteCommand(
        long IdMapInstitucionPeriodo,
        string StrNombreTitulo,
        string StrNumeroRegistroSolicitud,
        long IdTipoPatente,
        long IdEstatusPatente,
        DateTime DateFechaSolicitud,
        DateTime? DateFechaConcesion,
        string StrTitularPatente,
        Guid IdUsuarioRegistro,
        List<InventorPatenteDto> Inventores)
        : IRequest<ResponseWrapper<ReportePatenteDto>>;
}