using CodeInTasks.Infrastructure.EF;
using CodeInTasks.Infrastructure.Identity;
using CodeInTasks.Infrastructure.Queues;
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
            services.AddIdentity();
            services.AddQueues();

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

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //TODO: add authorization (JWT)
            //TODO: add authentication

            services.AddScoped<IIdentityService, IdentityService>();
        }

        private static void AddQueues(this IServiceCollection services)
        {
            services.AddScoped<ISolutionQueue, SolutionQueue>();
        }
    }
}
