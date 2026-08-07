using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatEstatusPatente.DTOs
{
    public sealed record CatEstatusPatenteDto(
    long Id,
    string StrValor,
    string StrDescripcion
);
}
