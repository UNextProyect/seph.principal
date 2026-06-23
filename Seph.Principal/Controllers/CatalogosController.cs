using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.CatSexo.Queries.GetCatsexo;

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
    }
}
