using Akka.Actor;
using Assigner.Application.IRepository;
using Assigner.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Actor
{
    public class UrlCreatedActor : ReceiveActor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public UrlCreatedActor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            ReceiveAsync<UrlCreatedEvent>(async evt =>
            {
                using var scope = _scopeFactory.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<IUrlRepository>();

                await repository.AddAsync(
                    new UrlMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        Slug = evt.Slug,
                        DestinationUrl = evt.DestinationUrl,
                        CreatedAt = evt.CreatedAt
                    });

                Sender.Tell(new UrlCreatedAck(evt.Slug, true));
            });
        }
    }
}
