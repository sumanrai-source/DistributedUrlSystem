using Akka.Actor;
using Assigner.Application.IRepository;
using Assigner.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Messages;
using Shared.Contracts.SlugData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Actor
{
    public class SlugResolverActor : ReceiveActor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SlugResolverActor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            ReceiveAsync<GetAllSlug>(HandleLookupAsync);

        }

        private async Task HandleLookupAsync(GetAllSlug message)
        {
            // Capture sender before awaiting
            var sender = Sender;

            using var scope = _scopeFactory.CreateScope();

            var _urlRepository = scope.ServiceProvider
                .GetRequiredService<ISlugRepository>();



            var slugs = await _urlRepository.GetAllSlugAsync();

            var slugList = slugs
                .Select(x => new SlugDto(
                    x.Value,      // or x.Slug if your entity property is named Slug
                    x.Status))
                .ToList();

            if (slugList.Count == 0)
            {
                sender.Tell(new SlugResponseNotFound());
                return;
            }

            sender.Tell(new SlugResponseFound(slugList));


        }
    }
}
