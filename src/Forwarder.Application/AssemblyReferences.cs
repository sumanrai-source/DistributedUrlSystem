using Microsoft.Extensions.DependencyInjection;

namespace Forwarder.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddForwarderApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReferences).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AssemblyReferences).Assembly));
            return services;
        }

    }
}
