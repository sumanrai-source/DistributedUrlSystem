using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Command.CreateShortUrl
{
    public record CreateShortUrlResponse
    (
        string slug,
        string shortUrl
        );
}
