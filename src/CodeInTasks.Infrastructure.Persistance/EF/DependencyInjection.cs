using CodeInTasks.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Infrastructure.Persistance.EF
{
    internal static class DependencyInjection
    {
        internal static void AddEfPersistance(this IServiceCollection services, IConfiguration config)
        {
            var sqlConnectionString = config.GetConnectionString(ConfigConstants.SqlConnectionString);

            services.AddDbContext<AppDbContext>(GetDbContextOptionsConfigurator(sqlConnectionString));

            services.AddScoped(typeof(IRepository<>), typeof(EfGenericRepository<>));
        }

        private static Action<DbContextOptionsBuilder> GetDbContextOptionsConfigurator(string sqlConnectionString)
        {
            void Configurator(DbContextOptionsBuilder contextOptions)
            {
                contextOptions.UseSqlServer(
                    sqlConnectionString,
                    serverOptions => serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            }

            return Configurator;
        }
    }
}
