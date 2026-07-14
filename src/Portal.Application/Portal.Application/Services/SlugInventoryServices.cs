using Portal.Application.Common;
using Portal.Application.Interfaces;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class SlugInventoryServices : ISlugInventoryServices
    {
        public Task<ApiResponse<IEnumerable<GetAvailableSlugsResponse>>> GetAllSlugAsync()
        {
            throw new NotImplementedException();
        }
    }
}
