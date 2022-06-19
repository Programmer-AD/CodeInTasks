using CodeInTasks.Builder.Infrastructure.Interfaces;
using CodeInTasks.Builder.Runtime.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure.Web
{
    public static class DependencyInjection
    {
        public static void AddSolutionStatusUpdater(this IServiceCollection services, IConfiguration config)
        {
            var solutionStatusUpdaterConfig = new SolutionStatusUpdaterConfig();
            config.Bind(BuilderConfigConstants.SolutionStatusUpdaterConfigSection, solutionStatusUpdaterConfig);
            services.AddSingleton(solutionStatusUpdaterConfig);

            services.AddSingleton<IAuthKeeper, AuthKeeper>();

            services.AddSingleton<ISolutionStatusUpdater, SolutionStatusUpdater>();
        }
    }
}

