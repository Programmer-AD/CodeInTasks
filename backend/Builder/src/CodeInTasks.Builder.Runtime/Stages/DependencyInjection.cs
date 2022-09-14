using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Runtime.Stages
{
    public static class DependencyInjection
    {
        public static void AddStages(this IServiceCollection services)
        {
            services.AddSingleton<IDownloadStage, DownloadStage>();
            services.AddSingleton<IBuildStage, BuildStage>();
            services.AddSingleton<IRunStage, RunStage>();
        }
    }
}
