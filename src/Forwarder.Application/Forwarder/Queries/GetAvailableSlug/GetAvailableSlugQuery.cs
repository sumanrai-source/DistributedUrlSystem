using Forwarder.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Forwarder.Queries.GetAvailableSlug
{
    public record GetAvailableSlugsQuery
    (
        ) : IRequest<ApiResponse<List<GetAvailableSlugsResponse>>>;
}
