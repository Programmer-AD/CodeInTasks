using CodeInTasks.Application.Mapping;
using CodeInTasks.Infrastructure.Mapping;
using CodeInTasks.Web.Filters;
using CodeInTasks.Web.Filters.ExceptionHandling;
using CodeInTasks.Web.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
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
                AddFilters(options.Filters);
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

        private static void AddFilters(FilterCollection filters)
        {
            filters.AddAuthorizeFilter();

            filters.Add<CurrentUserHolderInitFilter>();
            filters.Add<ChangeSaverFilter>();
            filters.Add<ExceptionHandlerFilter>();
        }

        private static void AddAuthorizeFilter(this FilterCollection filters)
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            var authFilter = new AuthorizeFilter(policy);

            filters.Add(authFilter);
        }
    }
}
