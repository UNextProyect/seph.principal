using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using Seph.Principal.Application.Features.Instituciones;
using Seph.Principal.Application.Features.Instituciones.Queries.GetInstituciones;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatSexo.Queries.GetCatsexo
{
    public sealed class CatSexoQueryHandler(ICatSexoRepository catSexoRepository)
        : IRequestHandler<GetCatSexoQuery, ResponseWrapper<IReadOnlyList<CatSexoDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<CatSexoDto>>> Handle(GetCatSexoQuery request, CancellationToken cancellationToken)
        {
            var CatSexo = await catSexoRepository.GetAllAsync(cancellationToken);

            IReadOnlyList<CatSexoDto> response = CatSexo.Select(x => new CatSexoDto(
                        x.Id,
                        x.StrValor,
                        x.StrDescripcion))
                    .ToList();

            return ResponseFactory.Success(
                response,
                "Catalogo de Sexo obtenido correctamente");
        }
    }
}
