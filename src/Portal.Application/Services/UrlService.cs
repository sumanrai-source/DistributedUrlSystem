using Akka.Actor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Portal.Application.Common;
using Portal.Application.Interfaces;
using Portal.Application.IRepository;
using Portal.Application.options;
using Portal.Application.Portal.Command.CreateShortUrl;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using Portal.Application.Portal.Queries.GetUrlMapping;
using Shared.Contracts.Events;
using Shared.Contracts.MappingUrl;
using Shared.Contracts.SlugData;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace Portal.Application.Services
{
    public class UrlService : IUrlService
    {
        private readonly IAssignerClientServices _assignerClient;
        private readonly ILogger<UrlService> _logger;
        private readonly ForwarderOptions _forwarderOptions;
        private readonly IAkkaActorProvider _akkaActorProvider;


        public UrlService(IAkkaActorProvider actorProvider,IAssignerClientServices assignerClientServices, ILogger<UrlService> logger, IOptions<ForwarderOptions> forwarderOptions)
        {
            _assignerClient = assignerClientServices;
            _akkaActorProvider = actorProvider;
            _logger = logger;
            _forwarderOptions = forwarderOptions.Value;

        }

        public async Task<ApiResponse<CreateShortUrlResponse>> CreateShortUrlAsync(CreateShortUrlCommand request,CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.url))
                throw new ArgumentException("URL is required.");

            // Ask Assigner service for an available slug
            var slug = await _assignerClient.AssignSlugAsync(cancellationToken);

            if (slug == null)
            {
                return ApiResponse<CreateShortUrlResponse>.FailResponse(
                    "NotFounds",
                    new List<string> { "NotFound","No record exists for the provided slug." }
                );
            }

            if (string.IsNullOrWhiteSpace(slug))
                throw new InvalidOperationException("No available slug.");


            var urlCreatedEvent = new UrlCreatedEvent(
                slug,
                request.url,
                DateTime.UtcNow);

            _logger.LogInformation(
                "Publishing UrlCreatedEvent. Slug: {Slug}",
                slug);

            _akkaActorProvider.Publish(urlCreatedEvent);

            _logger.LogInformation(
                "Created short URL. Slug: {Slug}, Destination: {Destination}",
                slug,
                request.url);

            var shortUrl = new Uri(new Uri(_forwarderOptions.BaseUrl), slug);

            var response = new CreateShortUrlResponse(
                slug,
                shortUrl.ToString());

            return ApiResponse<CreateShortUrlResponse>.SuccessResponse(
                response,
                "Short URL created successfully.");
        }

        public async Task<ApiResponse<List<GetUrlMappingResponse>>> GetAllUrlMappingAsync()
        {
            var response = await _akkaActorProvider.UrlMapping.Ask<object>(
                new GetMappingUrl(),
                TimeSpan.FromSeconds(5));

            if (response is UrlMappingNotFound)
            {
                return ApiResponse<List<GetUrlMappingResponse>>.FailResponse(
                    "No urlmapping found.");
            }

            if (response is not UrlMappingFound urlMappingResponse)
            {
                return ApiResponse<List<GetUrlMappingResponse>>.FailResponse(
                    "Invalid response from slug resolver.");
            }

            var result = urlMappingResponse.UrlMappingDTOs
                .Select(x => new GetUrlMappingResponse(
                    x.slug,
                    x.destinationUrl,
                    x.createdAt
                    ))
                .ToList();

            return ApiResponse<List<GetUrlMappingResponse>>.SuccessResponse(
                result,
                "UrlMapping retrieved successfully.");
        }
    }
}
