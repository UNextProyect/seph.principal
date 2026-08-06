namespace Seph.Principal.Application.Features.ReporteFinanza.DTOs
{
    /// <summary>
    /// Representa las estadísticas generales
    /// del reporte financiero.
    /// </summary>
    public sealed record ReporteFinanzaEstadisticasDto(
        /// <summary>
        /// Nombre del periodo seleccionado.
        /// </summary>
        string Periodo,

        /// <summary>
        /// Presupuesto anual registrado.
        /// </summary>
        decimal PresupuestoAnual,

        /// <summary>
        /// Subsidio estatal registrado.
        /// </summary>
        decimal SubsidioEstatal,

        /// <summary>
        /// Subsidio federal registrado.
        /// </summary>
        decimal SubsidioFederal,

        /// <summary>
        /// Ingresos propios registrados.
        /// </summary>
        decimal IngresosPropios,

        /// <summary>
        /// Gasto total ejercido.
        /// </summary>
        decimal GastoEjercido,

        /// <summary>
        /// Gasto ejercido por alumno.
        /// </summary>
        decimal GastoAlumno,

        /// <summary>
        /// Monto total de los adeudos.
        /// </summary>
        decimal MontoAdeudo);
}