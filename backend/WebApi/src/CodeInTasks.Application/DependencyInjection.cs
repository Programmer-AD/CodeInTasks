using CodeInTasks.Application.Enqueuers;
using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();
            services.AddEnqueuers();
            services.AddFiltration();

            return services;
        }
    }
}
