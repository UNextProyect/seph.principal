using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoContrato.DTOs
{
    public sealed record CatTipoContratoDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
