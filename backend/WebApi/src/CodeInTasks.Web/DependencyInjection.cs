using CodeInTasks.Application.Mapping;
using CodeInTasks.Web.Filters;
using CodeInTasks.Web.Filters.ExceptionHandling;
using CodeInTasks.Web.Mapping;

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

            return services;
        }

        private static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfile<ApplicationProfile>();
                mapperConfig.AddProfile<WebProfile>();
            });
        }
    }
}
