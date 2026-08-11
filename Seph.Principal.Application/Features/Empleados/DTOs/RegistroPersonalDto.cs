namespace Seph.Principal.Application.Features.Empleados.DTOs
{
    /// <summary>
    /// Fila del concentrado de registros de personal.
    /// Combina la información del empleado con su historial de contrato
    /// y los valores de los catálogos ya resueltos (texto listo para mostrar).
    /// Los campos del contrato son null cuando el registro quedó incompleto
    /// (se guardó el empleado pero no su historial de contrato).
    /// </summary>
    public sealed record RegistroPersonalDto(
        long IdEmpleado,
        string StrNombre,
        string StrApellidoPat,
        string StrApellidoMat,
        string StrCurp,
        string StrSexo,
        DateTime DateTimeFechaRegistro,
        bool BitActivo,
        bool BitDatosAcademicosCompletos,
        bool BitContratoCompleto,
        string? StrSNII,
        string? StrRutaIne,
        string? StrRutaFotografia,
        IReadOnlyList<string> PerfilesAcademicos,
        string? StrInstitucion,
        string? StrTipoPersonal,
        string? StrTipoContrato,
        string? StrArea,
        DateTime? DateFechaIngreso
    );
}
