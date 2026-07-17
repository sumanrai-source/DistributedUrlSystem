using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Command.CreateShortUrl
{
    public record CreateShortUrlRequest
    (
        string url
        );
}
