using Microsoft.Extensions.DependencyInjection;

namespace Assigner.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddAssignerApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

    }
}
