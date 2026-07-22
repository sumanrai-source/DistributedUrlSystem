using MediatR;
using Portal.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Queries.GetUrlMapping
{
    public record GetUrlMappingQuery
    (
        ) : IRequest<ApiResponse<List<GetUrlMappingResponse>>>;
}
