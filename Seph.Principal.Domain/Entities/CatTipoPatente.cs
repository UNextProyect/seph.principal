using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Entities
{
    public class CatTipoPatente
    {
        public long Id { get; set; }

        public string StrValor { get; set; } = string.Empty;

        public string StrDescripcion { get; set; } = string.Empty;

        #region Constructor
        public CatTipoPatente()
        {

        }

        public CatTipoPatente(int id, string strValor, string strDescripcion)
        {
            Id = id;
            StrValor = strValor;
            StrDescripcion = strDescripcion;
        }

        public CatTipoPatente(string strValor)
        {
            StrValor = strValor;
        }


        #endregion
    }
}
