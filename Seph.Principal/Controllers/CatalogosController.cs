using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.CatArea.Queries.GetArea;
using Seph.Principal.Application.Features.CatSexo.Queries.GetCatsexo;
using Seph.Principal.Application.Features.CatTipoContrato.Queries.GetCatTipoContrato;
using Seph.Principal.Application.Features.CatTipoPersonal.Queries.GetCatTipoPersonal;

namespace Seph.Principal.Controllers
{
    public sealed class CatalogosController(ISender sender) : ApiControllerBase
    {
        //[Authorize]
        [HttpGet("sexos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSexos(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetCatSexoQuery(), cancellationToken);
            return Ok(response);
        }

        //[Authorize]
        [HttpGet("tipos-personal")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTiposPersonal(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetCatTipoPersonalQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("tipos-contrato")]
        public async Task<IActionResult> GetTiposContrato(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetCatTipoContratoQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("areas")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAreas(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new GetCatAreaQuery(), cancellationToken);
            return Ok(response);
        }


    }
}