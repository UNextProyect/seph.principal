using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.ReportePatente.DTOs;
using System;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReportePatente.Commands
{
    /*
     * Actualiza un reporte de patente
     * mediante su identificador.
     */
    public sealed record UpdateReportePatenteCommand(
        long Id,
        long IdMapInstitucionPeriodo,
        string StrNombreTitulo,
        string StrNumeroRegistroSolicitud,
        long IdTipoPatente,
        long IdEstatusPatente,
        DateTime DateFechaSolicitud,
        DateTime? DateFechaConcesion,
        string StrTitularPatente,
        List<InventorPatenteDto> Inventores)
        : IRequest<ResponseWrapper<ReportePatenteDto>>;
}