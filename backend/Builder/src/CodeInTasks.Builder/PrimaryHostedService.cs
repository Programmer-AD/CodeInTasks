using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Builder
{
    internal class PrimaryHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime appLifetime;
        private readonly ILogger<PrimaryHostedService> logger;
        private readonly IBuilderService builderService;

        private int exitCode = 0;

        public PrimaryHostedService(
            IHostApplicationLifetime appLifetime,
            ILogger<PrimaryHostedService> logger,
            IBuilderService builderService)
        {
            this.appLifetime = appLifetime;
            this.logger = logger;
            this.builderService = builderService;
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
                }
                catch (Exception exception)
                {
                    exitCode = -1;

                    logger.LogCritical(exception, "Unhandled exception!");
                }
                finally
                {
                    appLifetime.StopApplication();
                }
            });
        }

        private Task RunAsync()
        {
            return builderService.RunAsync();
        }
    }
}
