using CodeInTasks.Builder.Infrastructure.Docker;
using CodeInTasks.Builder.Infrastructure.Git;
using CodeInTasks.Builder.Infrastructure.Web;
using CodeInTasks.Builder.Runtime.Abstractions;
using CodeInTasks.Shared.Queues;
using CodeInTasks.Shared.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddWrappers();

            var redisConfiguration = config.GetConnectionString(BuilderConfigConstants.RedisConnectionString);
            services.AddMessageQueues(redisConfiguration);

            services.AddSingleton<HttpClient>();

            services.AddGitRepositories();
            services.AddSolutionStatusUpdater(config);
            services.AddIsolatedExecutor();

            return services;
        }
    }
}

