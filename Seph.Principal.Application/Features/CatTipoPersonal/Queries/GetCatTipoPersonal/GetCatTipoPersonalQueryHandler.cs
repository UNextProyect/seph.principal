using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoPersonal.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPersonal.Queries.GetCatTipoPersonal
{
    public sealed class GetCatTipoPersonalQueryHandler(ICatTipoPersonalRepository catTipoPersonalRepository)
        : IRequestHandler<GetCatTipoPersonalQuery, ResponseWrapper<IReadOnlyList<CatTipoPersonalDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatTipoPersonalDto>>> Handle(
            GetCatTipoPersonalQuery request,
            CancellationToken cancellationToken)
        {
            var catTipoPersonal = await catTipoPersonalRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatTipoPersonalDto> response = catTipoPersonal
                .Select(x => new CatTipoPersonalDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Tipo de Personal obtenido correctamente");
        }
    }
}