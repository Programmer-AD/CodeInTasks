using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure.Execution
{
    public static class DependencyInjection
    {
        public static void AddIsolatedExecutor(this IServiceCollection services)
        {
            services.AddSingleton<IIsolatedExecutor, IsolatedExecutor>();
        }
    }
}

