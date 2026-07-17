using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Akka.Hosting;
using Assigner.Api.Akka;
using Assigner.Application;
using Assigner.Application.Actor;
using Assigner.Infrastructure;
using Assigner.Infrastructure.Data;
using Assigner.Infrastructure.DataSeeder;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Constants;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AssignerDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddSingleton<ActorRegistrys>();
//builder.Services.AddHostedService<AkkaHostedService>();


builder.Services
    .AddAssignerApplication()
    .AddAssignerInfrastructure();


builder.Services.AddAkka("ClusterSystem", (akkaBuilder, provider) =>
{
    akkaBuilder.AddHocon(
        @"
        akka {
            actor {
                provider = cluster
            }

            remote {
                dot-netty.tcp {
                    hostname = localhost
                    port = 4054
                }
            }

            cluster {
                seed-nodes = [
                    ""akka.tcp://ClusterSystem@localhost:4054""
                ]

                roles = [assigner]
            }
        }",
        HoconAddMode.Prepend);

    akkaBuilder.WithActors((system, registry) =>
    {
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        system.ActorOf(ClusterSingletonManager.Props(
        Props.Create(() => new UrlCreatedActor(scopeFactory)),
        PoisonPill.Instance,
        ClusterSingletonManagerSettings.Create(system)),
        ActorNames.UrlCreatedSingleton);
    });

    akkaBuilder.WithActors((system, registry) =>
    {
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        system.ActorOf(ClusterSingletonManager.Props(
        Props.Create(() => new UrlResolverActor(scopeFactory)),
        PoisonPill.Instance,
        ClusterSingletonManagerSettings.Create(system)),
        ActorNames.UrlResolverSingleton);
    });



    akkaBuilder.WithActors((system, registry) =>
    {
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        system.ActorOf(ClusterSingletonManager.Props(
        Props.Create(() => new SlugResolverActor(scopeFactory)),
        PoisonPill.Instance,
        ClusterSingletonManagerSettings.Create(system)),
        ActorNames.SlugResolverSingleton);
    });


    //akkaBuilder.WithActors((system, registry) =>
    //{
    //    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    //    var actor = system.ActorOf(ClusterSingletonManager.Props(
    //    Props.Create(() => new UrlCreatedActor(scopeFactory)),
    //    PoisonPill.Instance,
    //    ClusterSingletonManagerSettings.Create(system)),
    //    ActorNames.UrlCreatedSingleton);

    //    registry.Register<UrlCreatedActor>(actor);
    //});

    //akkaBuilder.WithActors((system, registry) =>
    //{
    //    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    //    var actor = system.ActorOf(ClusterSingletonManager.Props(
    //    Props.Create(() => new UrlResolverActor(scopeFactory)),
    //    PoisonPill.Instance,
    //    ClusterSingletonManagerSettings.Create(system)),
    //    ActorNames.UrlResolverSingleton);

    //    registry.Register<UrlResolverActor>(actor);
    //});



    //akkaBuilder.WithActors((system, registry) =>
    //{
    //    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    //    var actor = system.ActorOf(ClusterSingletonManager.Props(
    //    Props.Create(() => new SlugResolverActor(scopeFactory)),
    //    PoisonPill.Instance,
    //    ClusterSingletonManagerSettings.Create(system)),
    //    ActorNames.SlugResolverSingleton);

    //    registry.Register<SlugResolverActor>(actor);
    //});

});

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<AssignerDbContext>();

    await context.Database.MigrateAsync();
    await SlugSeeder.SeedAsync(context);
});





//var app = builder.Build();

//using var scope = app.Services.CreateScope();

//var context = scope.ServiceProvider
//    .GetRequiredService<AssignerDbContext>();

//await context.Database.MigrateAsync();
//await SlugSeeder.SeedAsync(context);




app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
