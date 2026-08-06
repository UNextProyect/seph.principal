namespace Seph.Principal.Application.Features.ReporteFinanza.DTOs
{
    /// <summary>
    /// Representa el comparativo de un indicador
    /// del reporte financiero entre el periodo
    /// actual y el periodo anterior.
    /// </summary>
    public sealed record ReporteFinanzaComparativoDto(
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
        decimal ValorActual,

        /// <summary>
        /// Nombre del periodo anterior.
        /// </summary>
        string? PeriodoAnterior,

        /// <summary>
        /// Valor del indicador en el periodo anterior.
        /// </summary>
        decimal? ValorAnterior,

        /// <summary>
        /// Diferencia entre ambos periodos.
        /// </summary>
        decimal Diferencia,

        /// <summary>
        /// Porcentaje de cambio respecto al periodo anterior.
        /// </summary>
        decimal PorcentajeCambio,

        /// <summary>
        /// Estado del indicador.
        /// </summary>
        string Estado);
}