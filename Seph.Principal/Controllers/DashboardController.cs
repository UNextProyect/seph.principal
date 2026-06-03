using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Features.Administration.Queries.GetDashboard;

namespace Seph.Principal.Controllers
{
    [Authorize(Policy = "Users.Read")]
    public sealed class DashboardController(ISender sender) : ApiControllerBase
    {
        [HttpGet("summary")]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
            => FromResponse(await sender.Send(new GetDashboardQuery(), cancellationToken));
    }

}
