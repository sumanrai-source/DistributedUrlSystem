using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Forwarder.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Messaging
{
    public class AkkaProvider : IAkkaProvider
    {
        public IActorRef UrlResolver { get; }

        public AkkaProvider(ActorSystem actorSystem)
        {
            UrlResolver = actorSystem.ActorOf(
               ClusterSingletonProxy.Props(
                   "/user/url-resolver-singleton",
                   ClusterSingletonProxySettings.Create(actorSystem)),
               "url-resolver-proxy");
        }
    }
}
