using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.HistorialContratos.DTOs
{
    public sealed record HistorialContratoResponse(
        long Id,
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
        Guid IdUsuarioRegistro
    );
}