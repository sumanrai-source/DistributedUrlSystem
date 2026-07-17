using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Command.CreateShortUrl.RequestCommandMapper
{
    public static class CreateShortUrlRequestMapper
    {
        public static CreateShortUrlCommand ToCommand(this CreateShortUrlRequest request)
        {
            return new CreateShortUrlCommand(request.url);
        }
    }
}
