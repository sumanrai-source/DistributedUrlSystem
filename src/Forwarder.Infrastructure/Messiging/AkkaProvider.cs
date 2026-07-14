using Akka.Actor;
using Forwarder.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Infrastructure.Messiging
{
    public class AkkaProvider : IActorProvider
    {
        public ActorSelection UrlResolver { get; }

        public AkkaProvider(ActorSystem actorSystem)
        {
            UrlResolver = actorSystem.ActorSelection(
                "akka.tcp://AssignerSystem@localhost:4054/user/url-resolver");
        }
    }
}
