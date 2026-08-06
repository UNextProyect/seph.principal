namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa un proyecto financiado
    /// registrado dentro de un reporte financiero.
    /// </summary>
    public class ProyectoFinanciado
    {
        /// <summary>
        /// Identificador único del proyecto financiado.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador del reporte financiero
        /// al que pertenece el proyecto.
        /// </summary>
        public long IdReporteFinanza { get; set; }

        /// <summary>
        /// Nombre del proyecto financiado.
        /// </summary>
        public string StrNombre { get; set; }

        /// <summary>
        /// Origen del financiamiento del proyecto.
        /// </summary>
        public string StrOrigenFinanciamiento { get; set; }

        /// <summary>
        /// Objetivo principal del proyecto financiado.
        /// </summary>
        public string StrObjetivo { get; set; }

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia vacía
        /// de la entidad ProyectoFinanciado.
        /// </summary>
        public ProyectoFinanciado()
        {
            StrNombre = string.Empty;
            StrOrigenFinanciamiento = string.Empty;
            StrObjetivo = string.Empty;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa del proyecto.
        /// </summary>
        public ProyectoFinanciado(
            long id,
            long idReporteFinanza,
            string strNombre,
            string strOrigenFinanciamiento,
            string strObjetivo)
        {
            Id = id;
            IdReporteFinanza = idReporteFinanza;
            StrNombre = strNombre;
            StrOrigenFinanciamiento = strOrigenFinanciamiento;
            StrObjetivo = strObjetivo;
        }

        #endregion
    }
}