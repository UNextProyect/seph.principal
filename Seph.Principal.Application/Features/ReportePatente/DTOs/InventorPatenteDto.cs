namespace Seph.Principal.Application.Features.ReportePatente.DTOs
{
    /// <summary>
    /// Representa un inventor
    /// asociado a un reporte de patente.
    /// </summary>
    public sealed class InventorPatenteDto
    {
        /// <summary>
        /// Nombre completo del inventor.
        /// </summary>
        public string StrNombreCompleto { get; set; } = string.Empty;
    }
}