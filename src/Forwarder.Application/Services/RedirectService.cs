using Akka.Actor;
using Forwarder.Application.Common;
using Forwarder.Application.Forwarder.Queries.DestinationUrlBySlug;
using Forwarder.Application.Forwarder.Queries.GetAvailableSlug;
using Forwarder.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Shared.Contracts.Messages;
using Shared.Contracts.SlugData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Services
{
    public class RedirectService : IRedirectService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<RedirectService> _logger;
        private readonly IActorProvider _actorProvider;



        public RedirectService(
        IActorProvider actorProvider,
        IMemoryCache cache,
        ILogger<RedirectService> logger)
        {
            _actorProvider = actorProvider;
            _cache = cache;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetAvailableSlugsResponse>>> GetAllSlugs(CancellationToken cancellationToken = default)
        {
            var response = await _actorProvider.SlugResolver.Ask<object>(
                new GetAllSlug(),
                TimeSpan.FromSeconds(5));

            if (response is SlugResponseNotFound)
            {
                return ApiResponse<List<GetAvailableSlugsResponse>>.FailResponse(
                    "No slugs found.");
            }

            if (response is not SlugResponseFound slugResponse)
            {
                return ApiResponse<List<GetAvailableSlugsResponse>>.FailResponse(
                    "Invalid response from slug resolver.");
            }

            var result = slugResponse.Slugs
                .Select(x => new GetAvailableSlugsResponse(
                    x.Slug,
                    x.Status))
                .ToList();

            return ApiResponse<List<GetAvailableSlugsResponse>>.SuccessResponse(
                result,
                "Slugs retrieved successfully.");
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

            var response = await _actorProvider.UrlResolver.Ask<object>(
                new GetUrlBySlug(slug),
                TimeSpan.FromSeconds(5));


            var slugResponse = await _actorProvider.SlugResolver.Ask<object>(
                new GetAllSlug(),
                TimeSpan.FromSeconds(5));



            if (response is UrlNotFound)
            {
                return ApiResponse<DestinationUrlBySlugResponse>.FailResponse(
                    "Slug not found.");
            }

            if (response is not UrlFound urlFound)
            {
                _logger.LogError(
                    "Unexpected response from UrlResolverActor.");

                return ApiResponse<DestinationUrlBySlugResponse>.FailResponse(
                    "Invalid response from URL resolver.");
            }

            _cache.Set(
                slug,
                urlFound.DestinationUrl,
                new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
                });


            return ApiResponse<DestinationUrlBySlugResponse>.SuccessResponse(
                new DestinationUrlBySlugResponse(
                    urlFound.DestinationUrl),
                "URL found.");

        }
    }
}
