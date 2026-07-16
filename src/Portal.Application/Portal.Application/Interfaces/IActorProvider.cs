using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface IActorProvider
    {
        IActorRef UrlResolver { get; }
        IActorRef SlugResolver { get; }
    }
}
