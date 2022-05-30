using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Runtime
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRuntime(this IServiceCollection services, IConfiguration config)
        {


            return services;
        }
    }
}
