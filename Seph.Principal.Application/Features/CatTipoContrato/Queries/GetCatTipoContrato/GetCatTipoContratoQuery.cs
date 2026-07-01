using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoContrato.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoContrato.Queries.GetCatTipoContrato
{
    public sealed record GetCatTipoContratoQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatTipoContratoDto>>>;
}

