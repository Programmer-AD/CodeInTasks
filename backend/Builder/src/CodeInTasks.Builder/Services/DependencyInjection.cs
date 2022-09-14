using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Builder.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IBuilderService, BuilderService>();
            services.AddSingleton<INamingService, NamingService>();

            return services;
        }
    }
}
