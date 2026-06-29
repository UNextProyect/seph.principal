using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    public record EmpleadoResponse(
        long Id,
        string StrNombre,
        string StrApellidoPat,
        string StrApellidoMat,
        string StrCurp
    );
}
