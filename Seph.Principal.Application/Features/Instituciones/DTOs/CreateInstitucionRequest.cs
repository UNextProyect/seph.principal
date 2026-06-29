namespace Seph.Principal.Application.Features.Instituciones.DTOs
{
    /// <summary>
    /// Cuerpo de la petición para crear una institución. Solo StrNombre e IdMunicipio son obligatorios.
    /// </summary>
    public sealed record CreateInstitucionRequest(
        string StrNombre,
        string? StrSiglas,
        string? StrCct,
        string? StrDireccion,
        DateTime? DateFechaCreacion,
        string? StrDecretoCreacion,
        string? StrSitioWeb,
        string? StrCorreoInstitucional,
        string? StrTelefonoInstitucional,
        long IdMunicipio);
}
