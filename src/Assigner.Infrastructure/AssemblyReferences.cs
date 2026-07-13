using Assigner.Application.Interfaces;
using Assigner.Application.IRepository;
using Assigner.Application.Services;
using Assigner.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Assigner.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddAssignerInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<ISlugService, SlugService>();
            services.AddScoped<ISlugRepository, SlugRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

    }
}
