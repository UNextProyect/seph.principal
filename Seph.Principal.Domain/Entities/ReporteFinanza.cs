using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa la información financiera
    /// registrada por una institución en un periodo.
    /// </summary>
    public class ReporteFinanza
    {
        /// <summary>
        /// Identificador único del reporte.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador de la relación entre
        /// la institución y el periodo activo.
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
        /// Fecha en la que se registró la información.
        /// </summary>
        public DateTime DateTimeFechaRegistro { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el registro.
        /// </summary>
        public Guid IdUsuarioRegistro { get; set; }

        /// <summary>
        /// Indica si el registro se encuentra activo.
        /// </summary>
        public bool BitActivo { get; set; }

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia vacía
        /// de la entidad ReporteFinanza.
        /// </summary>
        public ReporteFinanza()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa del reporte.
        /// </summary>
        public ReporteFinanza(
            long id,
            long idMapInstitucionPeriodo,
            decimal moneyPresupuestoAnual,
            decimal moneySubsidioEstatal,
            decimal moneySubsidioFederal,
            decimal moneyIngresosPropios,
            decimal moneyGastoEjercido,
            decimal moneyGastoAlumno,
            bool bitAdeudos,
            decimal moneyMontoAdeudo,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo)
        {
            Id = id;
            IdMapInstitucionPeriodo = idMapInstitucionPeriodo;
            MoneyPresupuestoAnual = moneyPresupuestoAnual;
            MoneySubsidioEstatal = moneySubsidioEstatal;
            MoneySubsidioFederal = moneySubsidioFederal;
            MoneyIngresosPropios = moneyIngresosPropios;
            MoneyGastoEjercido = moneyGastoEjercido;
            MoneyGastoAlumno = moneyGastoAlumno;
            BitAdeudos = bitAdeudos;
            MoneyMontoAdeudo = moneyMontoAdeudo;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
        }

        #endregion
    }
}