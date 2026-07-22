using Assigner.Application.Common;
using Assigner.Application.IRepository;
using Assigner.Domain.Entities;
using Assigner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Infrastructure.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AssignerDbContext _context;

        public UrlRepository(AssignerDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<bool>> AddAsync(
    UrlMapping mapping,
    CancellationToken cancellationToken = default)
        {
            await _context.UrlMappings.AddAsync(
                mapping,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);

            return ApiResponse<bool>.SuccessResponse(
                true,
                "URL mapping created successfully.");
        }

        public async Task<ApiResponse<UrlMapping>> GetBySlugAsync(
    string slug,
    CancellationToken cancellationToken = default)
        {
            var mapping = await _context.UrlMappings
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Slug == slug,
                    cancellationToken);

            if (mapping == null)
            {
                return ApiResponse<UrlMapping>.FailResponse(
                    "Slug not found.");
            }

            return ApiResponse<UrlMapping>.SuccessResponse(
                mapping,
                "Mapping found.");
        }

        public async Task<List<UrlMapping>> GetUrlMapping(CancellationToken cancellationToken = default)
        {
            return await _context.UrlMappings
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            
        }
    }
}
