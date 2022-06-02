using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Shared.Queues.Abstractions.Interfaces;
using CodeInTasks.Shared.Queues.Messages;

namespace CodeInTasks.Application.Enqueuers
{
    internal class SolutionCheckEnqueuer : ISolutionCheckEnqueuer
    {
        private readonly IRepository<TaskModel> taskRepository;
        private readonly IMessageQueue<SolutionCheckQueueMessage> messageQueue;

        public SolutionCheckEnqueuer(
            IMessageQueue<SolutionCheckQueueMessage> messageQueue,
            IRepository<TaskModel> taskRepository)
        {
            this.messageQueue = messageQueue;
            this.taskRepository = taskRepository;
        }

        public async Task EnqueueSolutionCheck(SolutionQueueDto solution)
        {
            var message = await MakeMessageAsync(solution);

            await messageQueue.PublishAsync(message);
        }

        internal async Task<SolutionCheckQueueMessage> MakeMessageAsync(SolutionQueueDto solution)
        {
            var taskId = solution.TaskId;
            var task = await GetTaskAsync(taskId);

            var message = new SolutionCheckQueueMessage()
            {
                SolutionId = solution.Id,
                Runner = task.Runner,

                TestRepositoryUrl = task.TestRepositoryUrl,
                TestRepositoryAccessToken = task.TestRepositoryAccessToken,

                UserRepositoryUrl = solution.RepositoryUrl,
                UserRepositoryAccessToken = solution.RepositoryAccessToken,
            };

            return message;
        }

        private async Task<TaskModel> GetTaskAsync(Guid taskId)
        {
            var taskModel = await taskRepository.GetAsync(taskId);

            return taskModel ?? throw new EntityNotFoundException(nameof(TaskModel), taskId);
        }
    }
}
