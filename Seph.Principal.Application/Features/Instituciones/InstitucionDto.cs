namespace Seph.Principal.Application.Features.Instituciones
{
    /// <summary>
    /// Record inmutable con los datos de la institución devueltos al cliente.
    /// </summary>
    public sealed record InstitucionDto(
        long Id,
        string StrNombre,
        string? StrSiglas,
        string? StrCct,
        string? StrDireccion,
        string? StrSitioWeb,
        string? StrCorreoInstitucional,
        string? StrTelefonoInstitucional,
        long IdMunicipio,
        bool BitActivo);
}
