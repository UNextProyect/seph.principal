using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    public record CreateEmpleadoRequest(
        string StrNombre,
        string StrApellidoPat,
        string StrApellidoMat,
        string StrCurp,
        long IdSexo,
        long IdInstitucion,
        DateTime DateTimeFechaRegistro,
        Guid IdUsuarioRegistro,
        bool BitActivo,
        DateTime DateTimeFechaBaja
    );
}
