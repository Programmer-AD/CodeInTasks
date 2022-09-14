using CodeInTasks.Infrastructure.Identity;
using CodeInTasks.Infrastructure.Persistance;
using CodeInTasks.Shared.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddPersistance(config);
            services.AddIdentity(config);
            services.AddWrappers();

            var redisConnectionString = config.GetConnectionString(ConfigConstants.RedisConnectionString);
            services.AddMessageQueues(redisConnectionString);

            return services;
        }
    }
}
