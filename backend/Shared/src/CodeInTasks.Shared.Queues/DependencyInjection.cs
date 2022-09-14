using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CodeInTasks.Shared.Queues
{
    public static class DependencyInjection
    {
        public static void AddMessageQueues(this IServiceCollection services, string redisConfiguration)
        {
            services.AddRedis(redisConfiguration);

            services.AddScoped(typeof(IMessageQueue<>), typeof(MessageQueue<>));
        }

        private static void AddRedis(this IServiceCollection services, string redisConfiguration)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration));

            services.AddScoped(provider => provider.GetService<IConnectionMultiplexer>().GetDatabase());
        }
    }
}
