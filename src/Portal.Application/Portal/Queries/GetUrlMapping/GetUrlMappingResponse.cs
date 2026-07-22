using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Queries.GetUrlMapping
{
    public record GetUrlMappingResponse
    (
        string slug,
        string destinationUrl,
        DateTime createdAt
        );
}
