using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tessa.Education.API.Controllers.V2
{
    /// <summary>
    /// Abstract parent conntroler with base methods
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class AbstractController : ControllerBase
    {
        [HttpGet("GetHost")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<string> GetHostId()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(token))
                throw new Exception("Unauthorized");

            return "Done!";
        }
    }
}
