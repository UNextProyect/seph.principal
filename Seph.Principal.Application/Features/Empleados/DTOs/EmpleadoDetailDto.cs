using System.Collections.Generic;

namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    /// <summary>
    /// Detalle completo de un empleado, usado para poblar el formulario
    /// de Información Personal al retomar un registro incompleto.
    /// </summary>
    public sealed record EmpleadoDetailDto(
    long Id,
    string StrNombre,
    string StrApellidoPat,
    string StrApellidoMat,
    string StrCurp,
    long IdSexo,
    string? StrSNII,
    string? StrRutaIne,
    string? StrRutaFotografia,
    IReadOnlyList<long> IdsPerfilAcademico,
    bool BitDatosAcademicosCompletos
);
}
