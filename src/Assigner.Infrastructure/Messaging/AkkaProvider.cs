using Akka.Actor;
using Assigner.Application.Actor;
using Assigner.Application.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Infrastructure.Messaging
{
    public class AkkaProvider
    {
        public AkkaProvider(
        ActorSystem system,
        IServiceScopeFactory scopeFactory)
        {
            system.ActorOf(
                Props.Create(() =>
                    new UrlResolverActor(scopeFactory)),
                "url-resolver");
        }
    }
}
