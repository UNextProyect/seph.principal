using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.MapInstitucionPeriodo.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Queries.GetPeriodoActivoInstitucion
{
    public sealed class GetPeriodoActivoInstitucionQueryHandler(
      IMapInstitucionPeriodoRepository mapInstitucionPeriodoRepository,
      ICatPeriodoRepository catPeriodoRepository)
      : IRequestHandler<
          GetPeriodoActivoInstitucionQuery,
          ResponseWrapper<PeriodoActivoInstitucionDto>>
    {
        public async Task<ResponseWrapper<PeriodoActivoInstitucionDto>> Handle(
            GetPeriodoActivoInstitucionQuery request,
            CancellationToken cancellationToken)
        {
            // Busca la relación activa para la institución.
            var mapInstitucionPeriodo =
                await mapInstitucionPeriodoRepository
                    .GetPeriodoActivoByInstitucionAsync(
                        request.IdInstitucion,
                        cancellationToken);

            if (mapInstitucionPeriodo is null)
            {
                return ResponseFactory.Failure<PeriodoActivoInstitucionDto>(
                    "La institución no tiene un periodo activo para captura.",
                    HttpStatusCode.NotFound);
            }

            // Obtiene los datos descriptivos del periodo.
            var periodo = await catPeriodoRepository.GetByIdAsync(
                mapInstitucionPeriodo.IdPeriodo,
                cancellationToken);

            if (periodo is null)
            {
                return ResponseFactory.Failure<PeriodoActivoInstitucionDto>(
                    "No fue posible encontrar la información del periodo activo.",
                    HttpStatusCode.NotFound);
            }

            var dto = new PeriodoActivoInstitucionDto(
                mapInstitucionPeriodo.Id,
                mapInstitucionPeriodo.IdInstitucion,
                mapInstitucionPeriodo.IdPeriodo,

                // Clave interna del periodo.
                periodo.StrValor,

                // Nombre descriptivo mostrado al usuario.
                periodo.StrDescripcion,

                mapInstitucionPeriodo.BitCapturaAbierta,
                mapInstitucionPeriodo.DateFechaApertura,
                mapInstitucionPeriodo.DateFechaCierre);

            return ResponseFactory.Success(
                dto,
                "Periodo activo obtenido correctamente");
        }
    }
}
