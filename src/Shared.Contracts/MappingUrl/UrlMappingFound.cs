using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.MappingUrl
{
    public sealed record UrlMappingDTO
        (
        string slug,
        string destinationUrl,
        DateTime createdAt
        );
    public sealed record UrlMappingFound
    (
        List<UrlMappingDTO> UrlMappingDTOs
        );
}
