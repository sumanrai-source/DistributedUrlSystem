using Akka.Actor;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Actors
{
    public class UrlEventPublisherActor : ReceiveActor
    {
        public UrlEventPublisherActor()
        {
            Receive<UrlCreatedEvent>(evt =>
            {
                Console.WriteLine(
                    $"Publishing URL event {evt.Slug}");
            });
        }
    }
}
