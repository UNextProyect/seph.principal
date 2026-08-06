namespace Seph.Principal.Application.Features.ReporteFinanza.DTOs
{
    /// <summary>
    /// Representa un proyecto financiado
    /// registrado dentro de un reporte financiero.
    /// </summary>
    public sealed class ProyectoFinanciadoDto
    {
        /// <summary>
        /// Identificador del proyecto financiado.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nombre del proyecto financiado.
        /// </summary>
        public string StrNombre { get; set; } = string.Empty;

        /// <summary>
        /// Origen del financiamiento del proyecto.
        /// </summary>
        public string StrOrigenFinanciamiento { get; set; } = string.Empty;

        /// <summary>
        /// Objetivo principal del proyecto financiado.
        /// </summary>
        public string StrObjetivo { get; set; } = string.Empty;
    }
}