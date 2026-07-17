using MediatR;
using Portal.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Command.CreateShortUrl
{
    public record CreateShortUrlCommand
    (
        string url
        ) : IRequest<ApiResponse<CreateShortUrlResponse>>;
}
