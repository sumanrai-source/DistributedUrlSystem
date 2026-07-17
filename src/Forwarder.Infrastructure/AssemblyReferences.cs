
using Microsoft.Extensions.DependencyInjection;

namespace Forwarder.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddForwarderInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            //services.AddScoped<IRedirectService, RedirectService>();

            //services.AddSingleton<IActorProvider, AkkaProvider>();


            return services;
        }

    }
}
