using CodeInTasks.Builder.Infrastructure;
using CodeInTasks.Builder.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeInTasks.Builder.Listening
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
                .AddRuntime(config)
                .AddInfrastructure(config)
                .AddEntry(config);
        }
    }
}