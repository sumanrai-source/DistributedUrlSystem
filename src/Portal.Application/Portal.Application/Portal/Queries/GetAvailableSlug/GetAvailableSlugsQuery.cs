using MediatR;
using Portal.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Queries.GetAvailableSlug
{
    public record GetAvailableSlugsQuery
    (
        ) : IRequest<ApiResponse<List<GetAvailableSlugsResponse>>>;
}
