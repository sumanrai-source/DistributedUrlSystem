using Akka.Actor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Portal.Application.Common;
using Portal.Application.Interfaces;
using Portal.Application.IRepository;
using Portal.Application.options;
using Portal.Application.Portal.Command.CreateShortUrl;
using Portal.Domain.Entities;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace Portal.Application.Services
{
    public class UrlService : IUrlService
    {
        private readonly IAssignerClientServices _assignerClient;
        private readonly IUrlRepository _urlRepository;
        private readonly ILogger<UrlService> _logger;
        private readonly ForwarderOptions _forwarderOptions;
        private readonly IAkkaActorProvider _akkaActorProvider;

        public UrlService(IAkkaActorProvider actorProvider,IAssignerClientServices assignerClientServices, IUrlRepository urlRepository, ILogger<UrlService> logger, IOptions<ForwarderOptions> forwarderOptions)
        {
            _assignerClient = assignerClientServices;
            _akkaActorProvider = actorProvider;
            _urlRepository = urlRepository;
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

            var mapping = new UrlMapping
            {
                Slug = slug,
                DestinationUrl = request.url,
                CreatedAt = DateTime.UtcNow
            };

            await _urlRepository.AddAsync(mapping, cancellationToken);

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
    }
}
