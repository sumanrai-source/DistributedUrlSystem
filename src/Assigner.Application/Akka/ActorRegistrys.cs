using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Akka
{
    public class ActorRegistrys
    {
        public IActorRef? UrlCreate { get; set; }
        public IActorRef? UrlResolver { get; set; }
        public IActorRef? SlugResolver { get; set; }
        public IActorRef? UrlMapping { get; set; }
    }
}
