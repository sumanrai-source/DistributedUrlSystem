using Akka.Actor;
using Assigner.Application.IRepository;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Actor
{
    public class UrlResolverActor: ReceiveActor
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public UrlResolverActor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            ReceiveAsync<GetUrlBySlug>(HandleLookupAsync);

            //Receive<GetUrlBySlug>(msg =>
            //{
            //    Console.WriteLine($"Received GetUrlBySlug: {msg.Slug}");
            //    Sender.Tell("OK");
            //});



        }

        private async Task HandleLookupAsync(GetUrlBySlug message)
        {
            // Capture sender before awaiting
            var sender = Sender;

            using var scope = _scopeFactory.CreateScope();

            var _urlRepository = scope.ServiceProvider
                .GetRequiredService<IUrlRepository>();

            var result = await _urlRepository.GetBySlugAsync(message.Slug);

            if (!result.Success)
            {
                sender.Tell(new UrlNotFound(message.Slug));
                return;
            }

            sender.Tell(new UrlFound(
                result.Data.Slug,
                result.Data.DestinationUrl));
        }

    }
}
