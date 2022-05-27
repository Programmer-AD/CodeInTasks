using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Seeding
{
    public static class DependencyInjection
    {
        //TODO: Make way to invoke seeding in Web or other dedicated project
        public static IServiceCollection AddSeeding(this IServiceCollection services)
        {
            services.AddScoped<Seeder>();

            return services;
        }
    }
}
