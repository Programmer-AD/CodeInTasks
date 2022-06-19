using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Seeding.Runner
{
    internal class SeedingHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime appLifetime;
        private readonly ILogger<SeedingHostedService> logger;

        //ServiceProvider instead of Seeder because Seeder is scoped
        private readonly IServiceProvider serviceProvider;

        private int exitCode = -1;

        public SeedingHostedService(
            IHostApplicationLifetime appLifetime,
            ILogger<SeedingHostedService> logger,
            IServiceProvider serviceProvider)
        {
            this.appLifetime = appLifetime;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            appLifetime.ApplicationStarted.Register(OnApplicationStarted);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Environment.ExitCode = exitCode;

            return Task.CompletedTask;
        }

        private void OnApplicationStarted()
        {
            Task.Run(async () =>
            {
                try
                {
                    await RunAsync();

                    exitCode = 0;
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Unhandled exception!");
                }
                finally
                {
                    appLifetime.StopApplication();
                }
            });
        }

        private Task RunAsync()
        {
            using var scope = serviceProvider.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            return seeder.SeedAsync();
        }
    }
}
