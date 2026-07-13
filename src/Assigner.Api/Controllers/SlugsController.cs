using Assigner.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assigner.Api.Controllers
{
    [ApiController]
    [Route("api/slugs")]
    public class SlugsController : ControllerBase
    {
        private readonly ISlugService _slugService;


        public SlugsController(ISlugService slugService)
        {
            _slugService = slugService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(CancellationToken cancellationToken)
        {
            var response = await _slugService.AssignSlugAsync(cancellationToken);

            return Ok(response);
        }
    }
}
