using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatArea.DTOs
{
    public sealed record CatAreaDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
