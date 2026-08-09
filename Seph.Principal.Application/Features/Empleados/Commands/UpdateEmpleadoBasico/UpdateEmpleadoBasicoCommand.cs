using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Empleados.Commands.UpdateEmpleadoBasico
{
    /// <summary>
    /// Actualiza los datos básicos (Nombre, apellidos, CURP, sexo) de un
    /// empleado ya registrado. Se usa al retomar un registro incompleto
    /// y editar la primera sub-pantalla de Información Personal.
    /// </summary>
    public sealed record UpdateEmpleadoBasicoCommand(
    long IdEmpleado,
    string StrNombre,
    string StrApellidoPat,
    string StrApellidoMat,
    string StrCurp,
    long IdSexo,
    string? StrRutaIne,
    string? StrRutaFotografia)
    : IRequest<ResponseWrapper<string>>;
}
