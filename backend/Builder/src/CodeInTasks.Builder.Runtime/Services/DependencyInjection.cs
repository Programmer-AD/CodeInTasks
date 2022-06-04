using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Runtime.Services
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ISolutionStatusTracerFactory, SolutionStatusTracerFactory>();
        }
    }
}
