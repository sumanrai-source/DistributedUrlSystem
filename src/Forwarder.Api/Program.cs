
using Akka.Hosting;
using Forwarder.Application;

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

builder.Services.AddMemoryCache();

builder.Services
    .AddForwarderApplication();

#region Cluster Configuration
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
                    port = 4056
                }
            }

            cluster {
                seed-nodes = [
                    ""akka.tcp://ClusterSystem@localhost:4054""
                ]
            }
        }",
        HoconAddMode.Prepend);


});

#endregion






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
