using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoPatente.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPatente.Queries.GetTipoPatente
{
    public sealed record GetCatTipoPatenteQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatTipoPatenteDto>>>;
}

