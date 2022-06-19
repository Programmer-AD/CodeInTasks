using CodeInTasks.Application.Mapping;
using CodeInTasks.Infrastructure.Mapping;
using CodeInTasks.Web.Filters;
using CodeInTasks.Web.Filters.ExceptionHandling;
using CodeInTasks.Web.Mapping;
using NLog.Extensions.Logging;

namespace CodeInTasks.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration config)
        {
            services.AddExceptionHandleInfos();

            services.AddMapping();
            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionHandlerFilterAttribute>();
                options.Filters.Add<ChangeSaverFilterAttribute>();
            });

            services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddNLog(config);
            });

            return services;
        }

        private static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfile<ApplicationProfile>();
                mapperConfig.AddProfile<InfrastructureProfile>();
                mapperConfig.AddProfile<WebProfile>();
            });
        }
    }
}
