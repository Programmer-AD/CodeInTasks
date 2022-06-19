using CodeInTasks.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeInTasks.Seeding.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var config = context.Configuration;

            services
                .AddSeedingRunner(config)
                .AddSeeding(config)
                .AddInfrastructure(config);
        }
    }
}
