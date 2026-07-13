using Forwarder.Application.Common;
using Forwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.IRepository
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
