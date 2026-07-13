using Assigner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.IRepository
{
    public interface ISlugRepository
    {
        Task<Slugs?> AssignAvailableSlugAsync(CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<Slugs> slugs,CancellationToken cancellationToken = default);
    }
}
