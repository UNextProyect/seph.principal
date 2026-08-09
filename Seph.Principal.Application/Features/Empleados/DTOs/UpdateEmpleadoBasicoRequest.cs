namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    public record UpdateEmpleadoBasicoRequest(
        string StrNombre,
        string StrApellidoPat,
        string StrApellidoMat,
        string StrCurp,
        long IdSexo,
        string? StrRutaIne,
        string? StrRutaFotografia
    );
}