using MediatR;
using Portal.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Queries.GetAvailableSlug
{
    public class GetAvailableSlugsQueryHandler : IRequestHandler<GetAvailableSlugsQuery, ApiResponse<List<GetAvailableSlugsResponse>>>
    {
        public Task<ApiResponse<List<GetAvailableSlugsResponse>>> Handle(GetAvailableSlugsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
