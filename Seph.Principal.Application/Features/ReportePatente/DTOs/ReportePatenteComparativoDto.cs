namespace Seph.Principal.Application.Features.ReportePatente.DTOs
{
    /// <summary>
    /// Representa el comparativo de un indicador
    /// del reporte de patentes entre el periodo
    /// actual y el periodo anterior.
    /// </summary>
    public sealed record ReportePatenteComparativoDto(
        /// <summary>
        /// Nombre del indicador comparado.
        /// </summary>
        string Indicador,

        /// <summary>
        /// Nombre del periodo actual.
        /// </summary>
        string PeriodoActual,

        /// <summary>
        /// Valor del indicador en el periodo actual.
        /// </summary>
        int ValorActual,

        /// <summary>
        /// Nombre del periodo anterior.
        /// </summary>
        string? PeriodoAnterior,

        /// <summary>
        /// Valor del indicador en el periodo anterior.
        /// </summary>
        int? ValorAnterior,

        /// <summary>
        /// Diferencia entre ambos periodos.
        /// </summary>
        int Diferencia,

        /// <summary>
        /// Porcentaje de cambio respecto al periodo anterior.
        /// </summary>
        decimal PorcentajeCambio,

        /// <summary>
        /// Estado del indicador.
        /// </summary>
        string Estado);
}