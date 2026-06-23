using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Application.Features.Instituciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatSexo.Queries.GetCatsexo
{
    public sealed record GetCatSexoQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatSexoDto>>>;
}

