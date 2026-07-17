using Forwarder.Api.BaseControllers;
using Forwarder.Application.forwarder.Queries.DestinationUrlBySlug;
using Forwarder.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Forwarder.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : BaseController
    {
        private readonly IRedirectService _redirectService;
        private readonly IMediator _mediator;

        public RedirectController(IRedirectService redirectService, IMediator mediator)
        {
            _mediator = mediator;
            _redirectService = redirectService;
        }


        [HttpGet("DestinationUrl")]
        public async Task<IActionResult> DestinationUrl([FromQuery] DestinationUrlBySlugDTOs destinationUrlBySlugDTOs, CancellationToken cancellationToken)
        {

            var query = new DestinationUrlBySlugQuery(destinationUrlBySlugDTOs);
            var filteredResult = await _mediator.Send(query);

            #region Switch
            return filteredResult switch
            {
                //{ Success: true, Data: not null } => Redirect(filteredResult.Data.originalUrl),
                { Success: true, Data: not null } => Ok(filteredResult.Data.originalUrl),
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
