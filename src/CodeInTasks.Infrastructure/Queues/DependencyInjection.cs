using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CodeInTasks.Infrastructure.Queues
{
    internal static class DependencyInjection
    {
        internal static void AddQueues(this IServiceCollection services, IConfiguration config)
        {
            var redisConnectionString = config.GetConnectionString(ConfigConstants.RedisConnectionString);

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));
            services.AddScoped(provider => provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

            services.AddScoped<ISolutionCheckQueue, SolutionCheckQueue>();
        }
    }
}
