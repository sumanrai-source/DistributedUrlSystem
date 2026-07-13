using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.IRepository
{
    public interface IAkkaActorProvider
    {
        IActorRef UrlCreatedListener { get; }
    }
}
