using CodeInTasks.Infrastructure.Identity;
using CodeInTasks.Infrastructure.Persistence.EF;
using CodeInTasks.Infrastructure.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddEfPersistance(config);
            services.AddIdentity(config);
            services.AddQueues(config);

            return services;
        }
    }
}
