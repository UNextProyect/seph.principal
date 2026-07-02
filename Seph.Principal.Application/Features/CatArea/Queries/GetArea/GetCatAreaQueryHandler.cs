using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatArea.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatArea.Queries.GetArea
{
    public sealed class GetCatAreaQueryHandler(ICatAreaRepository catAreaRepository)
        : IRequestHandler<GetCatAreaQuery, ResponseWrapper<IReadOnlyList<CatAreaDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatAreaDto>>> Handle(
            GetCatAreaQuery request,
            CancellationToken cancellationToken)
        {
            var catArea = await catAreaRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatAreaDto> response = catArea
                .Select(x => new CatAreaDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Areas obtenido correctamente");
        }
    }
}