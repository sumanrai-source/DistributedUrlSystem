using Akka.Actor;
using Akka.Configuration;
using Akka.Hosting;
using Forwarder.Application;
using Forwarder.Application.Actors;
using Forwarder.Application.IRepository;
using Forwarder.Infrastructure;
using Forwarder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Portal.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ForwarderDbContext>(options =>
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

builder.Services
    .AddForwarderApplication()
    .AddForwarderInfrastructure();




builder.Services.AddAkka("ForwarderSystem", (akkaBuilder, provider) =>
{
    akkaBuilder.AddHocon(
        @"
        akka {
            loglevel = INFO
            stdout-loglevel = INFO

            actor {
                provider = remote
            }

            remote {
                dot-netty.tcp {
                    hostname = localhost
                    port = 4054
                }
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
});




var app = builder.Build();

app.Services.GetRequiredService<IAkkaActorProvider>();






using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider
    .GetRequiredService<ForwarderDbContext>();

await context.Database.MigrateAsync();

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
