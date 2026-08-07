using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPatente.DTOs
{
    public sealed record CatTipoPatenteDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
