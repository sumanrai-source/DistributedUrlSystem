using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Portal.Application.IRepository;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Messaging
{
    public class AkkaProvider : IAkkaActorProvider
    {

        //public IActorRef UrlResolver { get; }

        public IActorRef SlugResolver { get; }

        public IActorRef UrlCreated { get; }

        public AkkaProvider(ActorSystem actorSystem)
        {

            UrlCreated = actorSystem.ActorOf(
               ClusterSingletonProxy.Props(
                   "/user/url-created-singleton",
                   ClusterSingletonProxySettings.Create(actorSystem)),
               "url-created-proxy");

            //UrlResolver = actorSystem.ActorOf(
            //   ClusterSingletonProxy.Props(
            //       "/user/url-resolver-singleton",
            //       ClusterSingletonProxySettings.Create(actorSystem)),
            //   "url-resolver-proxy");



            SlugResolver = actorSystem.ActorOf(
                ClusterSingletonProxy.Props(
                    "/user/slug-resolver-singleton",
                    ClusterSingletonProxySettings.Create(actorSystem)),
                "slug-resolver-proxy");
        }

    

        public void Publish(UrlCreatedEvent createdEvents)
        {
            UrlCreated.Tell(createdEvents);
        }
    }
}
