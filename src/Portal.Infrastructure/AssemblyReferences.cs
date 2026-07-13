using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Interfaces;
using Portal.Application.IRepository;
using Portal.Application.Services;
using Portal.Infrastructure.Clients;
using Portal.Infrastructure.Messaging;
using Portal.Infrastructure.Repository;

namespace Portal.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddPortalInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUrlRepository, UrlRepository>();

            services.AddScoped<IAssignerClientServices, AssignerClient>();

            services.AddScoped<IUrlService, UrlService>();

            services.AddSingleton<IAkkaActorProvider, AkkaProvider>();


            services.AddHttpClient<IAssignerClientServices, AssignerClientServices>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5137/");
            });

            return services;
        }

    }
}
