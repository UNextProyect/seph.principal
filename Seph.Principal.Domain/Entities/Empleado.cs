using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class Empleado
    {
        public Guid Id { get; set; }
        public string? StrNombre { get; set; }
        public string? StrApellidoPat { get; set; }
        public string? StrApellidoMat {  get; set; }
        public string? StrCurp { get; set; }
        public long IdSexo { get; set; }
        public DateTime DateTimeFechaRegistro { get; set; }
        public long IdUsuarioRegistro { get; set; }
        public bool BitActivo { get; set; }
        public DateTime DateTimeFechaBaja { get; set; }

        #region Constructor
        
        public Empleado()
        {

        }

        public Empleado(Guid id, string? strNombre, string? strApellidoPat, string? strApellidoMat, string? strCurp, int idSexo, DateTime dateTimeFechaRegistro, int idUsuarioRegistro, bool bitActivo, DateTime dateTimeFechaBaja)
        {
            Id = id;
            StrNombre = strNombre;
            StrApellidoPat = strApellidoPat;
            StrApellidoMat = strApellidoMat;
            StrCurp = strCurp;
            IdSexo = idSexo;
            DateTimeFechaRegistro = dateTimeFechaRegistro;
            IdUsuarioRegistro = idUsuarioRegistro;
            BitActivo = bitActivo;
            DateTimeFechaBaja = dateTimeFechaBaja;
        }

        public Empleado(string? strNombre)
        {
            StrNombre = strNombre;
        }


        #endregion
    }
}
