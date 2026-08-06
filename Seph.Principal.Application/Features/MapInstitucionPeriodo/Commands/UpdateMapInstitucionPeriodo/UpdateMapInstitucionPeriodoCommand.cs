using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.UpdateMapInstitucionPeriodo
{
    /// <summary>
    /// Actualiza una asignación
    /// de periodo por institución.
    /// </summary>
    public sealed record UpdateMapInstitucionPeriodoCommand(
        long Id,
        long IdInstitucion,
        long IdPeriodo,
        bool BitCapturaAbierta,
        DateTime? DateFechaApertura,
        DateTime? DateFechaCierre)
        : IRequest<ResponseWrapper<MapInstitucionPeriodoDto>>;
}
