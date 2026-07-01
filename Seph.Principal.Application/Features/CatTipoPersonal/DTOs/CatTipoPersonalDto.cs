using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPersonal.DTOs
{
    public sealed record CatTipoPersonalDto(
        long Id,
        string StrValor,
        string StrDescripcion
    );
}