﻿using CodeInTasks.Application.Mapping;
using CodeInTasks.Web.Mapping;

namespace CodeInTasks.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration config)
        {
            services.AddMapping();

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
