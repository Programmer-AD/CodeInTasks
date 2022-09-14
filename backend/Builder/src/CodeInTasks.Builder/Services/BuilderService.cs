using CodeInTasks.Shared.Queues.Abstractions.Interfaces;
using CodeInTasks.Shared.Queues.Messages;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Builder.Services
{
    public class BuilderService : IBuilderService
    {
        private readonly INamingService namingService;
        private readonly IMessageQueue<SolutionCheckQueueMessage> messageQueue;
        private readonly IRuntime runtime;
        private readonly ILogger<BuilderService> logger;

        public BuilderService(
            INamingService namingService,
            IMessageQueue<SolutionCheckQueueMessage> messageQueue,
            IRuntime runtime,
            ILogger<BuilderService> logger)
        {
            this.namingService = namingService;
            this.messageQueue = messageQueue;
            this.runtime = runtime;
            this.logger = logger;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var builderName = namingService.GetBuilderName();

            logger.LogInformation("Started builder with name \"{builderName}\"", builderName);

            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await messageQueue.GetMessageAsync(builderName);

                if (message != null)
                {
                    var checkQueueMessage = message.Data;

                    await runtime.HandleAsync(checkQueueMessage);

                    await messageQueue.AcknowledgeAsync(message);
                }
            }

            logger.LogInformation("Stopped builder \"{builderName}\": Cancellation requested", builderName);
        }
    }
}
