using CodeInTasks.Builder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CodeInTasks.Builder
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuilder(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();

            services.AddSingleton<PrimaryHostedService>();

            services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddNLog(config);
            });

            return services;
        }
    }
}
