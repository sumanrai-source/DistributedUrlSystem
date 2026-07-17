var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



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
                    port = 4055
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

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
