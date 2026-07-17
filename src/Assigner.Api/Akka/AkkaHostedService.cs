using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Assigner.Api.Akka;
using Assigner.Application.Actor;

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

        _registry.UrlCreate = urlCreate;
        _registry.UrlResolver = actor;
        _registry.SlugResolver = slugActor;

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _actorSystem.Terminate();
    }
}