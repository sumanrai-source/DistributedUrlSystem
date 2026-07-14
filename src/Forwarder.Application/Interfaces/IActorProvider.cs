using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Interfaces
{
    public interface IActorProvider
    {
        ActorSelection UrlResolver { get; }
    }
}
