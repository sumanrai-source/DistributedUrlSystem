using Akka.Actor;
using Forwarder.Application.Actors;
using Forwarder.Application.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Infrastructure.Messaging
{
    public class AkkaProvider : IAkkaActorProvider
    {
        public IActorRef UrlCreatedListener { get; }

        private readonly ActorSelection _urlCreatedListener;

        public AkkaProvider(ActorSystem actorSystem)
        {
            _urlCreatedListener = actorSystem.ActorSelection(
                "akka.tcp://ForwarderSystem@localhost:4054/user/urlCreatedListener");

        }
     

        public void Publish(object message)
        {
            _urlCreatedListener.Tell(message);
        }


    }
}
