using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.HistorialContratos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.HistorialContratos.Commands
{
    public sealed record CreateHistorialContratoCommand(
        DateTime DateFechaIngreso,
        DateTime DateFechaInicio,
        long IdEmpleado,
        long IdInstitucion,
        long IdTipoPersonal,
        int IdTipoContrato,
        string? StrOtroTipoContrato,
        long IdArea,
        DateTime DateTimeFechaRegistro,
        bool BitActivo,
        DateTime? DateTimeFechaBaja,
        Guid IdUsuarioRegistro)
        : IRequest<ResponseWrapper<HistorialContratoDto>>
    {
    }
}