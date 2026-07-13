using Assigner.Application;
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


builder.Services
    .AddAssignerApplication()
    .AddAssignerInfrastructure();
    



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
