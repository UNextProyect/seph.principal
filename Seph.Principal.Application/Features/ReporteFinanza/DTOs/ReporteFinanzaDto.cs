using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReporteFinanza.DTOs
{
    /// <summary>
    /// Representa la información de un
    /// reporte financiero.
    /// </summary>
    public sealed class ReporteFinanzaDto
    {
        /// <summary>
        /// Identificador del reporte.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador del mapa
        /// institución-periodo.
        /// </summary>
        public long IdMapInstitucionPeriodo { get; set; }

        /// <summary>
        /// Presupuesto anual de la institución.
        /// </summary>
        public decimal MoneyPresupuestoAnual { get; set; }

        /// <summary>
        /// Monto recibido mediante subsidio estatal.
        /// </summary>
        public decimal MoneySubsidioEstatal { get; set; }

        /// <summary>
        /// Monto recibido mediante subsidio federal.
        /// </summary>
        public decimal MoneySubsidioFederal { get; set; }

        /// <summary>
        /// Monto correspondiente a ingresos propios.
        /// </summary>
        public decimal MoneyIngresosPropios { get; set; }

        /// <summary>
        /// Monto total del gasto ejercido.
        /// </summary>
        public decimal MoneyGastoEjercido { get; set; }

        /// <summary>
        /// Monto del gasto ejercido por alumno.
        /// </summary>
        public decimal MoneyGastoAlumno { get; set; }

        /// <summary>
        /// Indica si la institución cuenta con adeudos.
        /// </summary>
        public bool BitAdeudos { get; set; }

        /// <summary>
        /// Monto total de los adeudos de la institución.
        /// </summary>
        public decimal MoneyMontoAdeudo { get; set; }

        /// <summary>
        /// Proyectos financiados registrados
        /// dentro del reporte.
        /// </summary>
        public List<ProyectoFinanciadoDto> ProyectosFinanciados { get; set; }
            = new();
    }
}