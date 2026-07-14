using Assigner.Application.IRepository;
using Assigner.Domain.Entities;
using Assigner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Assigner.Domain.Enums.HelperEnum;

namespace Assigner.Infrastructure.Repository
{
    public class SlugRepository : ISlugRepository
    {
        private readonly AssignerDbContext _context;

        public SlugRepository(AssignerDbContext context)
        {
            _context = context;
        }
        public async Task AddRangeAsync(IEnumerable<Slugs> slugs, CancellationToken cancellationToken = default)
        {
            await _context.Slugs.AddRangeAsync(slugs, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Slugs?> AssignAvailableSlugAsync(CancellationToken cancellationToken = default)
        {

            await using var transaction =
             await _context.Database.BeginTransactionAsync(cancellationToken);


            #region TestingApproach

            var mapping = await _context.UrlMappings
           .AsNoTracking()
           .FirstOrDefaultAsync(
               x => x.Slug == "c3f2db",
               cancellationToken);

            #endregion

            var slug = await _context.Slugs
                .Where(x => x.Status == SlugStatus.Available)
                .OrderBy(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (slug == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return null;
            }

            slug.Status = SlugStatus.Assigned;
            slug.AssignedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return slug;
        }


        public async Task<IEnumerable<Slugs>> GetAllSlugAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Slugs
            .AsNoTracking()
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
        }
    }
}
