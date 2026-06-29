using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;

namespace Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado
{
    public sealed record CreateEmpleadoCommand(
        string StrNombre,
        string StrApellidoPat,
        string StrApellidoMat,
        string StrCurp,
        long IdSexo,
        long IdInstitucion,
        DateTime DateTimeFechaRegistro,
        Guid IdUsuarioRegistro,
        bool BitActivo,
        DateTime DateTimeFechaBaja)
        : IRequest<ResponseWrapper<EmpleadoResponse>>;
}
