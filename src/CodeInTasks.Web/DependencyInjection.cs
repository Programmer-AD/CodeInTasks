using CodeInTasks.Application.Mapping;
using CodeInTasks.Web.Mapping;
using CodeInTasks.Web.Middleware.ExceptionHandling;

namespace CodeInTasks.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration config)
        {
            services.AddExceptionHandleInfos();

            services.AddMapping();
            services.AddControllers();

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
