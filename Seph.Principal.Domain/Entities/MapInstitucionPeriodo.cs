using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class MapInstitucionPeriodo
    {
        public long Id { get; set; }

        public long IdInstitucion { get; set; }
        public Institucion Institucion { get; set; } = null!;

        public CatPeriodo Periodo { get; set; } = null!;

        public long IdPeriodo { get; set; }

        public bool BitCapturaAbierta { get; set; }

        public DateTime? DateFechaApertura { get; set; }

        public DateTime? DateFechaCierre { get; set; }

        public DateTime DateTimeFechaRegistro { get; set; }

        public Guid IdUsuarioRegistro { get; set; }

        public bool BitActivo { get; set; }

        #region Constructor

        public MapInstitucionPeriodo()
        {

        }

        public MapInstitucionPeriodo(
            long id,
            long idInstitucion,
            long idPeriodo,
            bool bitCapturaAbierta,
            DateTime? dateFechaApertura,
            DateTime? dateFechaCierre,
            DateTime dateTimeFechaRegistro,
            Guid idUsuarioRegistro,
            bool bitActivo)
        {
            Id = id;
            IdInstitucion = idInstitucion;
            IdPeriodo = idPeriodo;
            BitCapturaAbierta = bitCapturaAbierta;
            DateFechaApertura = dateFechaApertura;
            DateFechaCierre = dateFechaCierre;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
        }

        #endregion
    }
}
