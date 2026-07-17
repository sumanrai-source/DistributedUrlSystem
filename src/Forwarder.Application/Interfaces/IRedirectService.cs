using Forwarder.Application.Common;
using Forwarder.Application.forwarder.Queries.DestinationUrlBySlug;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Interfaces
{
    public interface IRedirectService
    {
        Task<ApiResponse<DestinationUrlBySlugResponse>> GetDestinationUrlAsync(
            DestinationUrlBySlugDTOs destinationUrlBySlugDTOs,
            CancellationToken cancellationToken = default);

    }
}
