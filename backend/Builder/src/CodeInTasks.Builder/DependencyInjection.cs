using CodeInTasks.Builder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuilder(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();

            services.AddSingleton<PrimaryHostedService>();

            return services;
        }
    }
}
