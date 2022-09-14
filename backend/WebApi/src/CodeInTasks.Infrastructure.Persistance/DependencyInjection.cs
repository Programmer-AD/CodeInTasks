using CodeInTasks.Infrastructure.Persistance.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Infrastructure.Persistance
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration config)
        {
            services.AddEfPersistance(config);
        }
    }
}
