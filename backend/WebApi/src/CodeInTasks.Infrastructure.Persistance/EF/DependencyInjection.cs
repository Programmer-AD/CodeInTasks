using CodeInTasks.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Infrastructure.Persistance.EF
{
    internal static class DependencyInjection
    {
        public static void AddEfPersistance(this IServiceCollection services, IConfiguration config)
        {
            var sqlConnectionString = config.GetConnectionString(ConfigConstants.SqlConnectionString);
            var migrationAssemblyName = ConfigConstants.MigrationAssemblyName;

            services.AddDbContext<AppDbContext>(GetDbContextOptionsConfigurator(sqlConnectionString, migrationAssemblyName));
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<AppDbContext>());

            services.AddScoped(typeof(IRepository<>), typeof(EfGenericRepository<>));
        }

        private static Action<DbContextOptionsBuilder> GetDbContextOptionsConfigurator(string sqlConnectionString, string migrationAssemblyName)
        {
            void Configurator(DbContextOptionsBuilder contextOptions)
            {
                contextOptions.UseSqlServer(
                    sqlConnectionString,
                    serverOptions => serverOptions
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .MigrationsAssembly(migrationAssemblyName));
            }

            return Configurator;
        }
    }
}
