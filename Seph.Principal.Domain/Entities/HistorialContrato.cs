using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class HistorialContrato
    {
        public long Id { get; set; }

        public DateTime DateFechaIngreso { get; set; }

        public DateTime DateFechaInicio { get; set; }

        public long IdEmpleado { get; set; }

        public long IdInstitucion { get; set; }

        public long IdTipoPersonal { get; set; }

        public int IdTipoContrato { get; set; }

        public string StrOtroTipoContrato { get; set; } = string.Empty;

        public long IdArea { get; set; }

        public DateTime DateTimeFechaRegistro { get; set; }

        public bool BitActivo { get; set; }

        public DateTime? DateTimeFechaBaja { get; set; }

        public Guid IdUsuarioRegistro { get; set; }

        #region Constructor

        public HistorialContrato()
        {

        }

        public HistorialContrato(
            long id,
            DateTime dateFechaIngreso,
            DateTime dateFechaInicio,
            long idEmpleado,
            long idInstitucion,
            long idTipoPersonal,
            int idTipoContrato,
            string strOtroTipoContrato,
            long idArea,
            DateTime dateTimeFechaRegistro,
            bool bitActivo,
            DateTime? dateTimeFechaBaja,
            Guid idUsuarioRegistro)
        {
            Id = id;
            DateFechaIngreso = dateFechaIngreso;
            DateFechaInicio = dateFechaInicio;
            IdEmpleado = idEmpleado;
            IdInstitucion = idInstitucion;
            IdTipoPersonal = idTipoPersonal;
            IdTipoContrato = idTipoContrato;
            StrOtroTipoContrato = strOtroTipoContrato;
            IdArea = idArea;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            BitActivo = bitActivo;
            DateTimeFechaBaja = dateTimeFechaBaja;
            IdUsuarioRegistro = idUsuarioRegistro;
        }

        #endregion
    }
}