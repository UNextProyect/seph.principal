using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoContrato.DTOs;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoContrato.Queries.GetCatTipoContrato
{
    public sealed class GetCatTipoContratoQueryHandler(ICatTipoContratoRepository catTipoContratoRepository)
        : IRequestHandler<GetCatTipoContratoQuery, ResponseWrapper<IReadOnlyList<CatTipoContratoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatTipoContratoDto>>> Handle(
            GetCatTipoContratoQuery request,
            CancellationToken cancellationToken)
        {
            var catTipoContrato = await catTipoContratoRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatTipoContratoDto> response = catTipoContrato
                .Select(x => new CatTipoContratoDto(
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
