using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Infrastructure.Git
{
    public static class DependencyInjection
    {
        public static void AddGitRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IGitRepositoryFactory, GitRepositoryFactory>();
        }
    }
}

