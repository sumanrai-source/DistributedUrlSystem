using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.IRepository
{
    public interface IUrlRepository
    {
        Task AddAsync(UrlMapping mapping,CancellationToken cancellationToken = default);

        Task<UrlMapping?> GetBySlugAsync(string slug,CancellationToken cancellationToken = default);
    }
}
