using CodeInTasks.Infrastructure.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Seeding.Runner
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSeedingRunner(this IServiceCollection services)
        {
            services.AddHostedService<SeedingHostedService>();

            services.AddMapping();

            return services;
        }

        private static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfile<InfrastructureProfile>();
            });
        }
    }
}
