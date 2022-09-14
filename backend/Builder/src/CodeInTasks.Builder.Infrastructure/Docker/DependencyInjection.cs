using CodeInTasks.Builder.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure.Docker
{
    public static class DependencyInjection
    {
        public static void AddIsolatedExecutor(this IServiceCollection services)
        {
            services.AddSingleton<IProcessRunner, ProcessRunner>();

            services.AddSingleton<IDockerProvider, DockerProvider>();
        }
    }
}

