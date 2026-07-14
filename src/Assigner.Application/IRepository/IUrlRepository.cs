using Assigner.Application.Common;
using Assigner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.IRepository
{
    public interface IUrlRepository
    {
        Task<ApiResponse<UrlMapping>> GetBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);

        Task<ApiResponse<bool>> AddAsync(
            UrlMapping mapping,
            CancellationToken cancellationToken = default);
    }
}
