using Akka.Actor;
using Akka.Hosting;
using Assigner.Api.Akka;
using Assigner.Application;
using Assigner.Application.Actor;
using Assigner.Infrastructure;
using Assigner.Infrastructure.Data;
using Assigner.Infrastructure.DataSeeder;
using Microsoft.EntityFrameworkCore;


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

#region Actor create

//builder.Services.AddSingleton(provider =>
//{
//    return ActorSystem.Create("AssignerSystem");
//});

#endregion


//builder.Services.AddAkka("AssignerSystem", (akkaBuilder, provider) =>
//{
//    akkaBuilder.AddHocon(
//        @"
//        akka {
//            loglevel = INFO
//            stdout-loglevel = INFO

//            actor {
//                provider = remote
//            }

//            remote {
//                dot-netty.tcp {
//                    hostname = localhost
//                    port = 4054
//                }
//            }
//        }",
//        HoconAddMode.Prepend);

//    akkaBuilder.WithActors((system, registry) =>
//    {
//        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

//        var actor = system.ActorOf(
//            Props.Create(() => new UrlCreatedActor(scopeFactory)),
//            "urlCreatedListener");

//        var path = actor.Path.ToString();

//        Console.WriteLine($"Created actor: {actor.Path}");

//        registry.Register<UrlCreatedActor>(actor);
//    });

//    akkaBuilder.WithActors((system, registry) =>
//    {
//        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
//        var actor = system.ActorOf(
//            Props.Create(() => new UrlResolverActor(scopeFactory)),
//            "urlResolver");

//        registry.Register<UrlResolverActor>(actor);
//    });
//});

builder.Services.AddAkka("AssignerSystem", (akkaBuilder, provider) =>
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
                    ""akka.tcp://AssignerSystem@localhost:4054""
                ]

                roles = [assigner]
            }
        }",
        HoconAddMode.Prepend);

    akkaBuilder.WithActors((system, registry) =>
    {
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

        var actor = system.ActorOf(
            Props.Create(() => new UrlCreatedActor(scopeFactory)),
            "urlCreatedListener");

        var path = actor.Path.ToString();

        Console.WriteLine($"Created actor: {actor.Path}");

        registry.Register<UrlCreatedActor>(actor);
    });

    akkaBuilder.WithActors((system, registry) =>
    {
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        var actor = system.ActorOf(
            Props.Create(() => new UrlResolverActor(scopeFactory)),
            "url-resolver");

        registry.Register<UrlResolverActor>(actor);
    });
});





var app = builder.Build();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider
    .GetRequiredService<AssignerDbContext>();

await context.Database.MigrateAsync();
await SlugSeeder.SeedAsync(context);




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
