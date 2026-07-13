using Forwarder.Application.Common;
using Forwarder.Application.Forwarder.Queries.DestinationUrlBySlug;
using Forwarder.Application.Interfaces;
using Forwarder.Application.IRepository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Services
{
    public class RedirectService : IRedirectService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RedirectService> _logger;

        public RedirectService(
        IUrlRepository urlRepository,
        IMemoryCache cache,
        ILogger<RedirectService> logger)
        {
            _urlRepository = urlRepository;
            _cache = cache;
            _logger = logger;
        }

        public async Task<ApiResponse<DestinationUrlBySlugResponse>> GetDestinationUrlAsync(DestinationUrlBySlugDTOs destinationUrlBySlugDTOs,CancellationToken cancellationToken = default)
        {
            var slug = destinationUrlBySlugDTOs.slug;

            if (_cache.TryGetValue(slug, out string? destination))
            {
                _logger.LogInformation("Cache hit for slug {Slug}", slug);

                return ApiResponse<DestinationUrlBySlugResponse>.SuccessResponse(
                    new DestinationUrlBySlugResponse
                    (
                        originalUrl: destination!
                    ),
                    "URL found in cache.");
            }

            _logger.LogInformation("Cache miss for slug {Slug}", slug);

            var mapping = await _urlRepository.GetBySlugAsync(
                slug,
                cancellationToken);

            if (mapping == null)
            {
                return ApiResponse<DestinationUrlBySlugResponse>.FailResponse(
                    "Slug not found.");
            }

            _cache.Set(slug,mapping.Data.DestinationUrl,new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
                });

            return ApiResponse<DestinationUrlBySlugResponse>.SuccessResponse(
                new DestinationUrlBySlugResponse
                (
                    originalUrl : mapping.Data.DestinationUrl
                ),
                "URL found.");
        }
    }
}
