using Akka.Actor;
using Assigner.Application.IRepository;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.MappingUrl;
using Shared.Contracts.SlugData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Actor
{
    public class UrlMappingActor : ReceiveActor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public UrlMappingActor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            ReceiveAsync<GetMappingUrl>(HandleLookupAsync);

        }

        private async Task HandleLookupAsync(GetMappingUrl message)
        {
            // Capture sender before awaiting
            var sender = Sender;

            using var scope = _scopeFactory.CreateScope();

            var _urlRepository = scope.ServiceProvider
                .GetRequiredService<IUrlRepository>();



            var urls = await _urlRepository.GetUrlMapping();

            var slugList = urls
                .Select(x => new UrlMappingDTO(
                    x.Slug,      
                    x.DestinationUrl,
                    x.CreatedAt
                    
                    ))
                .ToList();

            if (slugList.Count == 0)
            {
                sender.Tell(new UrlMappingNotFound());
                return;
            }

            sender.Tell(new UrlMappingFound(slugList));


        }
    }
}
