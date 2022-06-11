using CodeInTasks.Builder.Runtime.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure.Web
{
    public static class DependencyInjection
    {
        public static void AddSolutionStatusUpdater(this IServiceCollection services, IConfiguration config)
        {
            var solutionStatusUpdater = config.GetValue<SolutionStatusUpdaterConfig>(BuilderConfigConstants.SolutionStatusUpdaterConfigSection);
            
            services.AddSingleton(solutionStatusUpdater);

            services.AddSingleton<IAuthorizationKeeper, AuthorizationKeeper>();

            services.AddSingleton<ISolutionStatusUpdater, SolutionStatusUpdater>();
        }
    }
}

