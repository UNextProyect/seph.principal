using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Representa la información de una patente
    /// registrada por una institución en un periodo.
    /// </summary>
    public class ReportePatente
    {
        /// <summary>
        /// Identificador único de la patente.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador de la relación entre
        /// la institución y el periodo activo.
        /// </summary>
        public long IdMapInstitucionPeriodo { get; set; }

        /// <summary>
        /// Nombre o título oficial de la patente.
        /// </summary>
        public string StrNombreTitulo { get; set; } = string.Empty;

        /// <summary>
        /// Número de registro o solicitud
        /// asignado a la patente.
        /// </summary>
        public string StrNumeroRegistroSolicitud { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del catálogo
        /// de tipos de patente.
        /// </summary>
        public long IdTipoPatente { get; set; }

        /// <summary>
        /// Identificador del catálogo
        /// de estatus de patente.
        /// </summary>
        public long IdEstatusPatente { get; set; }

        /// <summary>
        /// Fecha en la que se presentó
        /// la solicitud de la patente.
        /// </summary>
        public DateTime DateFechaSolicitud { get; set; }

        /// <summary>
        /// Fecha en la que fue concedida la patente.
        /// Puede permanecer vacía cuando aún no ha sido concedida.
        /// </summary>
        public DateTime? DateFechaConcesion { get; set; }

        /// <summary>
        /// Nombre de la persona física o moral
        /// titular de los derechos de la patente.
        /// </summary>
        public string StrTitularPatente { get; set; } = string.Empty;

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
        /// de la entidad ReportePatente.
        /// </summary>
        public ReportePatente()
        {

        }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad
        /// con la información completa de la patente.
        /// </summary>
        public ReportePatente(
            long id,
            long idMapInstitucionPeriodo,
            string strNombreTitulo,
            string strNumeroRegistroSolicitud,
            long idTipoPatente,
            long idEstatusPatente,
            DateTime dateFechaSolicitud,
            DateTime? dateFechaConcesion,
            string strTitularPatente,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo)
        {
            Id = id;
            IdMapInstitucionPeriodo = idMapInstitucionPeriodo;
            StrNombreTitulo = strNombreTitulo;
            StrNumeroRegistroSolicitud = strNumeroRegistroSolicitud;
            IdTipoPatente = idTipoPatente;
            IdEstatusPatente = idEstatusPatente;
            DateFechaSolicitud = dateFechaSolicitud;
            DateFechaConcesion = dateFechaConcesion;
            StrTitularPatente = strTitularPatente;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
        }

        #endregion
    }
}