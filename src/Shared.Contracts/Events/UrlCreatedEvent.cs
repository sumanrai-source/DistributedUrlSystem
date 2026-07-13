using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events
{
    public record UrlCreatedEvent
    (
        string Slug,
        string DestinationUrl,
        DateTime CreatedAt
        );
}
