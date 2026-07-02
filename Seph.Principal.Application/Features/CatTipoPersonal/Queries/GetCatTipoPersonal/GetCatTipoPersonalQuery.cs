using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoPersonal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPersonal.Queries.GetCatTipoPersonal
{
    public sealed record GetCatTipoPersonalQuery()
        : IRequest<ResponseWrapper<IReadOnlyList<CatTipoPersonalDto>>>;
}
