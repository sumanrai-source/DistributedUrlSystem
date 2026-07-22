using Akka.Actor;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.IRepository
{
    public interface IAkkaActorProvider
    {
        IActorRef UrlCreated { get; }
        //IActorRef UrlResolver { get; }
        IActorRef SlugResolver { get; }
        IActorRef UrlMapping { get; }

        void Publish(UrlCreatedEvent createdEvents);
    }
}
