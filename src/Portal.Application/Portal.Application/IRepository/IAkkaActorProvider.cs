using Akka.Actor;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.IRepository
{
    public interface IAkkaActorProvider
    {
        void Publish(UrlCreatedEvent createdEvents);
    }
}
