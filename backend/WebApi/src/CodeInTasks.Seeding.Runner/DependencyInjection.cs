using CodeInTasks.Infrastructure.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CodeInTasks.Seeding.Runner
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSeedingRunner(this IServiceCollection services, IConfiguration config)
        {
            services.AddHostedService<SeedingHostedService>();

            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfile<InfrastructureProfile>();
            });

            services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddNLog(config);
            });

            return services;
        }
    }
}
