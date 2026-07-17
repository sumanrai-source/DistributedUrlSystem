using Akka.Actor;

namespace Assigner.Api.Akka
{
    public class ActorRegistrys
    {
        public IActorRef? UrlCreate { get; set; }
        public IActorRef? UrlResolver { get; set; }
        public IActorRef? SlugResolver { get; set; }
    }
}