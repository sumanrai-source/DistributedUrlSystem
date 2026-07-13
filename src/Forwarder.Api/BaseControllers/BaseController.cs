using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forwarder.Api.BaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandlerFailure(IEnumerable<string> errors)
        {
            if(errors.Any(errors=>errors.Contains("Unauthorized"))) {
                var rawMessage = errors
                    .FirstOrDefault(e=> e.Contains("Unauthorized", StringComparison.OrdinalIgnoreCase));

                var cleanMessage = ExtractCleanMessage(rawMessage);

                return StatusCode(StatusCodes.Status401Unauthorized,
                    new {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = cleanMessage });
            }
            else if(errors.Any(error=>error.Contains("NotFound", StringComparison.OrdinalIgnoreCase))) {


                var rawMessage = errors.Count() > 1
                    ? errors.ElementAt(1)
                    : errors.FirstOrDefault();
                var cleanMessage = ExtractCleanMessage(rawMessage);
                return StatusCode(StatusCodes.Status404NotFound,
                    new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = cleanMessage
                    });
            }
            else
            {
                return BadRequest(new { Errors = errors });
            }
        }

        private static string ExtractCleanMessage(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
                return "An error occurred.";
            var parts = rawMessage.IndexOf(',');
            if (parts >= 0 && parts < rawMessage.Length - 1)
            {
                return rawMessage[(parts + 1)..].Trim();
            }
            return rawMessage.Trim();
        }
    }
}
