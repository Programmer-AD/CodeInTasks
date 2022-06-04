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

        //TODO: Make AuthUserName retrieve (probably form url)
        internal async Task<SolutionCheckQueueMessage> MakeMessageAsync(SolutionQueueDto solution)
        {
            var taskId = solution.TaskId;
            var task = await GetTaskAsync(taskId);

#warning This should not be empty
            var testRepositoryAuthUserName = string.Empty;
            var solutionRepositoryAuthUserName = string.Empty;

            var message = new SolutionCheckQueueMessage()
            {
                SolutionId = solution.Id,
                Runner = task.Runner,

                TestRepositoryInfo = new(task.TestRepositoryUrl, testRepositoryAuthUserName, solution.RepositoryAuthPassword),
                SolutionRepositoryInfo = new(solution.RepositoryUrl, solutionRepositoryAuthUserName, solution.RepositoryAuthPassword),
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
