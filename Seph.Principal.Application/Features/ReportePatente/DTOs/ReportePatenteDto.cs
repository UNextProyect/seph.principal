using System;
using System.Collections.Generic;

namespace Seph.Principal.Application.Features.ReportePatente.DTOs
{
    /// <summary>
    /// Representa la información de un
    /// reporte de patente.
    /// </summary>
    public sealed class ReportePatenteDto
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
        /// Nombre o título oficial de la patente.
        /// </summary>
        public string StrNombreTitulo { get; set; } = string.Empty;

        /// <summary>
        /// Número de registro o solicitud
        /// asignado a la patente.
        /// </summary>
        public string StrNumeroRegistroSolicitud { get; set; }
            = string.Empty;

        /// <summary>
        /// Identificador del tipo de patente.
        /// </summary>
        public long IdTipoPatente { get; set; }

        /// <summary>
        /// Identificador del estatus de la patente.
        /// </summary>
        public long IdEstatusPatente { get; set; }

        /// <summary>
        /// Fecha en la que se presentó
        /// la solicitud de la patente.
        /// </summary>
        public DateTime DateFechaSolicitud { get; set; }

        /// <summary>
        /// Fecha en la que fue concedida la patente.
        /// </summary>
        public DateTime? DateFechaConcesion { get; set; }

        /// <summary>
        /// Nombre de la persona física o moral
        /// titular de los derechos de la patente.
        /// </summary>
        public string StrTitularPatente { get; set; }
            = string.Empty;

        /// <summary>
        /// Inventores asociados a la patente.
        /// </summary>
        public List<InventorPatenteDto> Inventores { get; set; }
            = new();
    }
}