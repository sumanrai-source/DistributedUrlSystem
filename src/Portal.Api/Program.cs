using Akka.Actor;
using Akka.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Portal.Application;
using Portal.Application.Actors;
using Portal.Application.Interfaces;
using Portal.Application.IRepository;
using Portal.Application.options;
using Portal.Infrastructure;
using Portal.Infrastructure.Clients;
using Akka.Hosting;

var builder = WebApplication.CreateBuilder(args);


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


#region Register Options
builder.Services
    .AddOptions<ForwarderOptions>()
    .Bind(builder.Configuration.GetSection(ForwarderOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<AssignerOptions>()
    .Bind(builder.Configuration.GetSection(AssignerOptions.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.BaseUrl),
        "Assigner BaseUrl is required.")
    .ValidateOnStart();
#endregion


builder.Services
    .AddHttpClient<IAssignerClientServices, AssignerClient>((serviceProvider, client) =>
    {
        var options = serviceProvider
            .GetRequiredService<IOptions<AssignerOptions>>()
            .Value;

        client.BaseAddress = new Uri(options.BaseUrl);

        client.Timeout = TimeSpan.FromSeconds(10);
    });

builder.Services.AddAkka("PortalSystem", (akkaBuilder, provider) =>
{
    akkaBuilder.AddHocon(
        @"
        akka {
            actor {
                provider = remote
            }

            remote {
                dot-netty.tcp {
                    hostname = localhost
                    port = 0
                }
            }
        }",
        HoconAddMode.Prepend);
});





builder.Services.
    AddPortalApplication()
    .AddPortalInfrastructure();


builder.Services.AddSingleton<IAkkaActorProvider, Portal.Infrastructure.Messaging.AkkaProvider>();

var app = builder.Build();


//await context.Database.MigrateAsync();

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
