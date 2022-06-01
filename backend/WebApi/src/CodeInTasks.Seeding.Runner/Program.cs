using CodeInTasks.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeInTasks.Seeding.Runner
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices)
                .Build()
                .Run();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var config = context.Configuration;

            services
                .AddHostedService<SeedingHostedService>()
                .AddSeeding()
                .AddInfrastructure(config);
        }
    }
}
