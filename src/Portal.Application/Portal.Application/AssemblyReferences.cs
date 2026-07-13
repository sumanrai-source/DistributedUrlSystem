using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Portal.Command.CreateShortUrl;

namespace Portal.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddPortalApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReferences).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AssemblyReferences).Assembly));
            services.AddScoped<IValidator<CreateShortUrlCommand>, CreateShortUrlCommandValidator>();
            return services;
        }

    }
}
