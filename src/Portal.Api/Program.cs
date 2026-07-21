

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


//#region Register Options
//builder.Services
//    .AddOptions<ForwarderOptions>()
//    .Bind(builder.Configuration.GetSection(ForwarderOptions.SectionName))
//    .ValidateDataAnnotations()
//    .ValidateOnStart();

//builder.Services
//    .AddOptions<AssignerOptions>()
//    .Bind(builder.Configuration.GetSection(AssignerOptions.SectionName))
//    .Validate(options => !string.IsNullOrWhiteSpace(options.BaseUrl),
//        "Assigner BaseUrl is required.")
//    .ValidateOnStart();
//#endregion


//builder.Services
//    .AddHttpClient<IAssignerClientServices, AssignerClient>((serviceProvider, client) =>
//    {
//        var options = serviceProvider
//            .GetRequiredService<IOptions<AssignerOptions>>()
//            .Value;

//        client.BaseAddress = new Uri(options.BaseUrl);

//        client.Timeout = TimeSpan.FromSeconds(10);
//    });


//builder.Services.
//    AddPortalApplication()
//    .AddPortalInfrastructure();



//#region Cluster Configuration
//builder.Services.AddAkka("ClusterSystem", (akkaBuilder, provider) =>
//{
//    akkaBuilder.AddHocon(
//        @"
//        akka {
//            actor {
//                provider = cluster
//            }

//            remote {
//                dot-netty.tcp {
//                    hostname = localhost
//                    port = 4055
//                }
//            }

//            cluster {
//                seed-nodes = [
//                    ""akka.tcp://ClusterSystem@localhost:4054""
//                ]
//            }
//        }",
//        HoconAddMode.Prepend);


//});

//#endregion


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
