using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Listening
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEntry(this IServiceCollection services, IConfiguration config)
        {


            return services;
        }
    }
}
