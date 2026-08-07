namespace Seph.Principal.Application.Features.ReportePatente.DTOs
{
    /// <summary>
    /// Representa las estadísticas generales
    /// del reporte de patentes.
    /// </summary>
    public sealed record ReportePatenteEstadisticasDto(
        /// <summary>
        /// Nombre del periodo seleccionado.
        /// </summary>
        string Periodo,

        /// <summary>
        /// Total de patentes registradas.
        /// </summary>
        int TotalPatentes);
}