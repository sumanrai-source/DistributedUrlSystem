using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Interfaces;
using Portal.Application.Portal.Command.CreateShortUrl;
using Portal.Application.Portal.Command.CreateShortUrl.RequestCommandMapper;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using Portal.Application.Portal.Queries.GetUrlMapping;
using Portal.Server.BaseControllers;

namespace Portal.Server.Controllers
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
        [HttpPost("Create")]
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



        [HttpGet("AllSlug")]
        public async Task<IActionResult> AllSlug()
        {

            var query = new GetAvailableSlugsQuery();
            var filteredResult = await _mediator.Send(query);

            #region Switch
            return filteredResult switch
            {
                { Success: true, Data: not null } => Ok(filteredResult.Data),
                { Success: true, Data: null, Message: not null } => new JsonResult(new
                {
                    Data = (object?)null,
                    StatusCode = StatusCodes.Status200OK,
                    Message = filteredResult.Message
                }),
                { Success: false, Errors: not null } => HandlerFailure(filteredResult.Errors),
                _ => BadRequest("Invalid Fields")
            };

            #endregion

        }

        [HttpGet("AllUrlMapping")]
        public async Task<IActionResult> AllUrlMapping()
        {

            var query = new GetUrlMappingQuery();
            var filteredResult = await _mediator.Send(query);

            #region Switch
            return filteredResult switch
            {
                { Success: true, Data: not null } => Ok(filteredResult.Data),
                { Success: true, Data: null, Message: not null } => new JsonResult(new
                {
                    Data = (object?)null,
                    StatusCode = StatusCodes.Status200OK,
                    Message = filteredResult.Message
                }),
                { Success: false, Errors: not null } => HandlerFailure(filteredResult.Errors),
                _ => BadRequest("Invalid Fields")
            };

            #endregion

        }

    }
}
