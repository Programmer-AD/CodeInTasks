using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuilder(this IServiceCollection services, IConfiguration config)
        {


            return services;
        }
    }
}
