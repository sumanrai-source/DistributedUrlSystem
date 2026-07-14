using Portal.Application.Common;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface ISlugInventoryServices
    {
        Task<ApiResponse<IEnumerable<GetAvailableSlugsResponse>>> GetAllSlugAsync();
    }
}
