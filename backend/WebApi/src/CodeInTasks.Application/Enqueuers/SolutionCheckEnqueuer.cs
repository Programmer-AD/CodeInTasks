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

        public async Task EnqueueSolutionCheck(SolutionQueueModel solution)
        {
            var message = await MakeMessageAsync(solution);

            await messageQueue.PublishAsync(message);
        }

        private async Task<SolutionCheckQueueMessage> MakeMessageAsync(SolutionQueueModel solution)
        {
            var taskId = solution.TaskId;
            var task = await GetTaskAsync(taskId);

            var testRepositoryAuthUserName = GetGitUserName(task.TestRepositoryUrl);
            var solutionRepositoryAuthUserName = GetGitUserName(solution.RepositoryUrl);

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

        private static string GetGitUserName(string repositoryUrl)
        {
            var url = new Uri(repositoryUrl);

            var nameSegment = url.Segments[1];

            var name = nameSegment[..^1];

            return name;
        }
    }
}
