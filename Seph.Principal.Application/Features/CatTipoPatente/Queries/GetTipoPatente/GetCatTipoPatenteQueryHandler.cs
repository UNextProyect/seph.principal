using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatTipoPatente.DTOs;
using Seph.Principal.Application.Features.CatTipoPatente.Queries.GetTipoPatente;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatTipoPatente.Queries.GetTipoPatente
{
    public sealed class GetCatTipoPatenteQueryHandler(ICatTipoPatenteRepository catTipoPatenteRepository)
        : IRequestHandler<GetCatTipoPatenteQuery, ResponseWrapper<IReadOnlyList<CatTipoPatenteDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatTipoPatenteDto>>> Handle(
            GetCatTipoPatenteQuery request,
            CancellationToken cancellationToken)
        {
            var catTipoPatente = await catTipoPatenteRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatTipoPatenteDto> response = catTipoPatente
                .Select(x => new CatTipoPatenteDto(
                    x.Id,
                    x.StrValor,
                    x.StrDescripcion))
                .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Tipo de Patente obtenido correctamente");
        }
    }
}