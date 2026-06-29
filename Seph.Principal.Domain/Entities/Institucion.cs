using System;

namespace Seph.Principal.Domain.Entities
{
    /// <summary>
    /// Institución educativa. Mapea a la tabla real dbo.Institucion (DB-first).
    /// Nota: la columna BytesLogo (varbinary) no se gestiona vía API por ahora, por eso no se mapea.
    /// </summary>
    public class Institucion
    {
        public long Id { get; set; }

        public string StrNombre { get; set; } = string.Empty;
        public string? StrSiglas { get; set; }
        public string? StrCct { get; set; }
        public string? StrDireccion { get; set; }
        public DateTime? DateFechaCreacion { get; set; }
        public string? StrDecretoCreacion { get; set; }
        public string? StrSitioWeb { get; set; }
        public string? StrCorreoInstitucional { get; set; }
        public string? StrTelefonoInstitucional { get; set; }
        public long IdMunicipio { get; set; }
        public DateTime DateTimeFechaRegistro { get; set; }
        public bool BitActivo { get; set; }
        public DateTime? DateTimeFechaBaja { get; set; }

        #region Constructor
        public Institucion()
        {

        }
        #endregion
    }
}
