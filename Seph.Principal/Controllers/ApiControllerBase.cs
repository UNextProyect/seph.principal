using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult FromResponse<T>(ResponseWrapper<T> response)
            => StatusCode((int)response.StatusCode, response);
    }

}
