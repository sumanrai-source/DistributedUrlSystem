using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Messages
{
    public sealed record UrlFound
    (
        string slug,
        string DestinationUrl
        );
}
