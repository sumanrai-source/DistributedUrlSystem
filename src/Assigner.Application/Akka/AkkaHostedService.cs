using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Assigner.Application.Actor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Application.Akka
{
    public class AkkaHostedService : IHostedService
    {
        private readonly ActorSystem _actorSystem;
        private readonly ActorRegistrys _registry;
        private readonly IServiceScopeFactory _scopeFactory;

        public AkkaHostedService(
            ActorSystem actorSystem,
            ActorRegistrys registry,
            IServiceScopeFactory scopeFactory)
        {
            _actorSystem = actorSystem;
            _registry = registry;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var urlCreate = _actorSystem.ActorOf(
                ClusterSingletonManager.Props(
                    Props.Create(() =>
                        new UrlCreatedActor(_scopeFactory)),
                    PoisonPill.Instance,
                    ClusterSingletonManagerSettings
                        .Create(_actorSystem)
                        .WithRole("assigner")),
                "url-create");


            var actor = _actorSystem.ActorOf(
                ClusterSingletonManager.Props(
                    Props.Create(() =>
                        new UrlResolverActor(_scopeFactory)),
                    PoisonPill.Instance,
                    ClusterSingletonManagerSettings
                        .Create(_actorSystem)
                        .WithRole("assigner")),
                "url-resolver");

            var slugActor = _actorSystem.ActorOf(
                ClusterSingletonManager.Props(
                    Props.Create(() =>
                        new UrlResolverActor(_scopeFactory)),
                    PoisonPill.Instance,
                    ClusterSingletonManagerSettings
                        .Create(_actorSystem)
                        .WithRole("assigner")),
                "slug-resolver");


            var urlMapping = _actorSystem.ActorOf(
             ClusterSingletonManager.Props(
                 Props.Create(() =>
                     new UrlMappingActor(_scopeFactory)),
                 PoisonPill.Instance,
                 ClusterSingletonManagerSettings
                     .Create(_actorSystem)
                     .WithRole("assigner")),
             "url-mapping");

            _registry.UrlCreate = urlCreate;
            _registry.UrlResolver = actor;
            _registry.SlugResolver = slugActor;
            _registry.UrlMapping = urlMapping;

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _actorSystem.Terminate();
        }
    }
}
