using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure.Docker
{
    public static class DependencyInjection
    {
        public static void AddIsolatedExecutor(this IServiceCollection services)
        {
            services.AddSingleton<IDockerProvider, DockerProvider>();
        }
    }
}

