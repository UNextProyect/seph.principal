using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatEstatusPatente.DTOs;


namespace Seph.Principal.Application.Features.CatEstatusPatente.Queries.GetEstatusPatente
{

    public sealed record GetEstatusPatenteQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatEstatusPatenteDto>>>;
}


