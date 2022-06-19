using CodeInTasks.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Seeding
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSeeding(this IServiceCollection services, IConfiguration config)
        {
            var seedingOptions = new SeedingOptions();
            config.Bind(ConfigConstants.SeedingOptionsSection, seedingOptions);
            services.AddSingleton(seedingOptions);

            services.AddScoped<Seeder>();

            return services;
        }
    }
}
