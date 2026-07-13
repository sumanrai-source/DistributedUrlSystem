using Forwarder.Application.Interfaces;
using Forwarder.Application.IRepository;
using Forwarder.Application.Services;
using Forwarder.Infrastructure.Messaging;
using Forwarder.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Forwarder.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddForwarderInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IRedirectService, RedirectService>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddSingleton<AkkaProvider>();
            services.AddSingleton<IAkkaActorProvider,AkkaProvider>();

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

    }
}
