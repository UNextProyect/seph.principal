using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Auth.DTOs
{
    public sealed record CatSexoDto(
    int Id,
    string StrValor,
    string StrDescripcion
);
}
