using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Seeding
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSeeding(this IServiceCollection services)
        {
            services.AddScoped<Seeder>();

            return services;
        }
    }
}
