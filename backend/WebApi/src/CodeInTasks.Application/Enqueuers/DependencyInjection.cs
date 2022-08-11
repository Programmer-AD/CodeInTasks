using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.Enqueuers
{
    internal static class DependencyInjection
    {
        public static void AddEnqueuers(this IServiceCollection services)
        {
            services.AddScoped<ISolutionCheckEnqueuer, SolutionCheckEnqueuer>();
        }
    }
}
