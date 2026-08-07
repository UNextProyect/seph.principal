using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa la relación entre una patente
    /// y las personas que participaron como inventores.
    /// </summary>
    public class InventorPatente
    {
        /// <summary>
        /// Identificador único del registro.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador del reporte de patente.
        /// </summary>
        public long IdPatente { get; set; }

        /// <summary>
        /// Nombre completo del inventor.
        /// </summary>
        public string StrNombreCompleto { get; set; } = string.Empty;

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia vacía
        /// de la entidad InventorPatente.
        /// </summary>
        public InventorPatente()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa del inventor.
        /// </summary>
        public InventorPatente(
            long id,
            long idPatente,
            string strNombreCompleto)
        {
            Id = id;
            IdPatente = idPatente;
            StrNombreCompleto = strNombreCompleto;
        }

        #endregion
    }
}