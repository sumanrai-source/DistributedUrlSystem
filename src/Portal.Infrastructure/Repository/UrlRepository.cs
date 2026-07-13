using Microsoft.EntityFrameworkCore;
using Portal.Application.IRepository;
using Portal.Domain.Entities;
using Portal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly PortalDbContext _context;

        public UrlRepository(PortalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UrlMapping mapping, CancellationToken cancellationToken = default)
        {

            var mappingData = new UrlMapping(
             id: Guid.NewGuid().ToString(),
             slug: mapping.Slug,
             destinationUrl: mapping.DestinationUrl,
             createdAt: DateTime.UtcNow
         );
            await _context.UrlMappings.AddAsync(mappingData, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<UrlMapping?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.UrlMappings
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Slug == slug,
                cancellationToken);
        }
    }
}
