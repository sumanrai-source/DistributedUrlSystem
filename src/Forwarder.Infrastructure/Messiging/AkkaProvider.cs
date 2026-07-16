using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Forwarder.Application.Interfaces;

namespace Forwarder.Infrastructure.Messiging
{
    public class AkkaProvider : IActorProvider
    {
        public IActorRef UrlResolver { get; }
        public IActorRef SlugResolver { get; }

        public AkkaProvider(ActorSystem actorSystem)
        {
            UrlResolver = actorSystem.ActorOf(
                ClusterSingletonProxy.Props(
                    "/user/url-resolver-singleton",
                    ClusterSingletonProxySettings.Create(actorSystem)),
                "url-resolver-proxy");

            SlugResolver = actorSystem.ActorOf(
                ClusterSingletonProxy.Props(
                    "/user/slug-resolver-singleton",
                    ClusterSingletonProxySettings.Create(actorSystem)),
                "slug-resolver-proxy");
        }
    }
}