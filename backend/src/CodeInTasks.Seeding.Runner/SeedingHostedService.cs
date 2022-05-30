using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Seeding.Runner
{
    internal class SeedingHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime appLifetime;
        private readonly ILogger<SeedingHostedService> logger;
        private readonly Seeder seeder;

        private int exitCode = -1;

        public SeedingHostedService(
            IHostApplicationLifetime appLifetime,
            ILogger<SeedingHostedService> logger,
            Seeder seeder)
        {
            this.appLifetime = appLifetime;
            this.logger = logger;
            this.seeder = seeder;
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
            return seeder.SeedAsync();
        }
    }
}
