using Forwarder.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.forwarder.Queries.DestinationUrlBySlug
{
    public record DestinationUrlBySlugQuery
    (
        DestinationUrlBySlugDTOs request
        ) : IRequest<ApiResponse<DestinationUrlBySlugResponse>>;
}
