using CodeInTasks.Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddEfPersistance(options => config.Bind(ConfigSections.EfDbOptions, options));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static void AddEfPersistance(
            this IServiceCollection services,
            Action<EfDbOptions> configureEfDbOptions)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EfGenericRepository<>));
            services.Configure(configureEfDbOptions);
        }
    }
}
