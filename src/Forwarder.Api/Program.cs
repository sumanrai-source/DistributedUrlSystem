
using Akka.Actor;
using Akka.Hosting;
using Forwarder.Application;
using Forwarder.Application.Interfaces;
using Forwarder.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

builder.Services
    .AddForwarderApplication()
    .AddForwarderInfrastructure();

//builder.Services.AddAkka("ForwarderSystem", (akkaBuilder, provider) =>
//{
//    akkaBuilder.AddHocon(
//        @"
//        akka {
//            actor {
//                provider = remote
//            }

//            remote {
//                dot-netty.tcp {
//                    hostname = localhost
//                    port = 4055
//                }
//            }
//        }",
//        HoconAddMode.Prepend);
//});

builder.Services.AddAkka("ForwarderSystem", (akkaBuilder, provider) =>
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
                    port = 4055
                }
            }

            cluster {
                seed-nodes = [
                    ""akka.tcp://ForwarderSystem@localhost:4055""
                ]
            }
        }",
        HoconAddMode.Prepend);
});




var app = builder.Build();

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
