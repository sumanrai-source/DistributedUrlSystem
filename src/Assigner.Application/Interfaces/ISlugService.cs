using Assigner.Application.Common;
using Assigner.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Interfaces
{
    public interface ISlugService
    {
        Task<ApiResponse<AssignSlugResponse>> AssignSlugAsync(CancellationToken cancellationToken = default);
    }
}
