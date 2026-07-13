using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.BaseControllers;
using Portal.Application.DTOs;
using Portal.Application.Interfaces;
using Portal.Application.Portal.Command.CreateShortUrl;
using Portal.Application.Portal.Command.CreateShortUrl.RequestCommandMapper;

namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : BaseController
    {
        private readonly IUrlService _urlService;
        private readonly IMediator _mediator;

        public UrlsController(IUrlService urlService, IMediator mediator)
        {
            _mediator = mediator;
            _urlService = urlService;
        }
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateShortUrlRequest request, CancellationToken cancellationToken)
        {

            var command = request.ToCommand();
            var addResult = await _mediator.Send(command);

            #region Switch
            return addResult switch
            {
                { Success: true, Data: not null } => CreatedAtAction(nameof(Create), new { id = addResult.Data?.shortUrl }, addResult),
                { Success: true, Data: null, Message: not null } => new JsonResult(new
                {
                    Data = addResult.Data,
                    StatusCode = StatusCodes.Status200OK,
                    Message = addResult.Message
                }),
                { Success: false, Errors: not null } => HandlerFailure(addResult.Errors),
                _ => BadRequest("Invalid Fields")
            };

            #endregion

        }

    }
}
