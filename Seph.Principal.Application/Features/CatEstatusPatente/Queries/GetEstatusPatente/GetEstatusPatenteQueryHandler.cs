using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatEstatusPatente.DTOs;
using Seph.Principal.Application.Features.CatEstatusPatente.Queries.GetEstatusPatente;
using Seph.Principal.Application.Features.CatArea.DTOs;
using Seph.Principal.Domain.Repositories;


namespace Seph.Principal.Application.Features.CatEstatusPatente.Queries.GetEstatusPatente
{
    public sealed class GetCatEstatusPatenteQueryHandler(ICatEstatusPatenteRepository catEstatusPatenteRepository)
        : IRequestHandler<GetEstatusPatenteQuery, ResponseWrapper<IReadOnlyList<CatEstatusPatenteDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatEstatusPatenteDto>>> Handle(
            GetEstatusPatenteQuery request,
            CancellationToken cancellationToken)
        {
            var catEstatus = await catEstatusPatenteRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatEstatusPatenteDto> response = catEstatus
                .Select(x => new CatEstatusPatenteDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Estatus de las Patentes obtenido correctamente");
        }
    }
}