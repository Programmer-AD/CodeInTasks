using CodeInTasks.Domain.Enums;
using CodeInTasks.Shared.Queues.Messages;
using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime
{
    public class Runtime : IRuntime
    {
        private readonly IGitRepositoryFactory gitRepositoryFactory;
        private readonly IIsolatedExecutor isolatedExecutor;
        private readonly ISolutionStatusUpdater solutionStatusUpdater;

        public Runtime(
            IGitRepositoryFactory gitRepositoryFactory,
            IIsolatedExecutor isolatedExecutor,
            ISolutionStatusUpdater solutionStatusUpdater)
        {
            this.gitRepositoryFactory = gitRepositoryFactory;
            this.isolatedExecutor = isolatedExecutor;
            this.solutionStatusUpdater = solutionStatusUpdater;
        }

        //TODO: Make status updating parallel with actions
        //TODO: Make way to handle download/build/... errors
        public async Task HandleAsync(SolutionCheckQueueMessage checkQueueMessage)
        {
            var solutionId = checkQueueMessage.SolutionId;

            var gitRepositoryFolder = GetGitRepositoryFolder(solutionId);

            await UpdateSolutionStatus(solutionId, TaskSolutionStatus.Downloading);
            await DownloadRepositoryAsync(checkQueueMessage, gitRepositoryFolder);

            var solutionInstanceName = solutionId.ToString("N");

            await UpdateSolutionStatus(solutionId, TaskSolutionStatus.Building);
            await BuildAsync(gitRepositoryFolder, solutionInstanceName);

            await UpdateSolutionStatus(solutionId, TaskSolutionStatus.Running);
            await RunAsync(solutionInstanceName);
        }

        private Task UpdateSolutionStatus(Guid solutionId, TaskSolutionStatus solutionStatus)
        {
            var solutionStatusModel = new SolutionStatusUpdateModel()
            {
                Id = solutionId,
                Status = solutionStatus,
            };

            return solutionStatusUpdater.UpdateStatusAsync(solutionStatusModel);
        }

        private Task PublishSolutionResult(Guid solutionId, Action<SolutionStatusUpdateModel> configureResult)
        {
            var solutionStatusModel = new SolutionStatusUpdateModel()
            {
                Id = solutionId,
                Status = TaskSolutionStatus.Finished,
                FinishTime = DateTime.UtcNow,
            };

            configureResult(solutionStatusModel);

            return solutionStatusUpdater.UpdateStatusAsync(solutionStatusModel);
        }

        //TODO: Add download error handling
        //TODO: Add download timeout
        private async Task DownloadRepositoryAsync(SolutionCheckQueueMessage checkQueueMessage, string gitRepositoryFolder)
        {
            var gitRepository = gitRepositoryFactory.GetRepository(gitRepositoryFolder);

            await CloneRepositoryAsync(checkQueueMessage.TestRepositoryInfo, gitRepository);
            
            var lastTestCommitId = gitRepository.GetLastCommitId();

            await PullRepositoryAsync(checkQueueMessage.SolutionRepositoryInfo, gitRepository);

            var allFilesPaths = new[] { "*" };
            gitRepository.CheckoutPaths(lastTestCommitId, allFilesPaths);
        }

        private static string GetGitRepositoryFolder(Guid solutionId)
        {
            var idString = solutionId.ToString("N");
            var result = Path.Combine(RuntimeConstants.DownloadFolder, idString);

            return result;
        }

        private static Task CloneRepositoryAsync(RepositoryInfo repositoryInfo, IGitRepository gitRepository)
        {
            var repositoryUrl = repositoryInfo.RepositoryUrl;
            var repositoryAuth = new GitAuthCredintials(repositoryInfo.AuthUserName, repositoryInfo.AuthPassword);

            return gitRepository.CloneAsync(repositoryUrl, repositoryAuth, CancellationToken.None);
        }

        private static Task PullRepositoryAsync(RepositoryInfo repositoryInfo, IGitRepository gitRepository)
        {
            var repositoryUrl = repositoryInfo.RepositoryUrl;
            var repositoryAuth = new GitAuthCredintials(repositoryInfo.AuthUserName, repositoryInfo.AuthPassword);

            return gitRepository.PullAsync(repositoryUrl, repositoryAuth, CancellationToken.None);
        }

        //TODO: Make build timeout
        //TODO: Make building error handling
        private Task BuildAsync(string folderPath, string instanceName)
        {
            return isolatedExecutor.BuildAsync(folderPath, instanceName, CancellationToken.None);
        }

        //TODO: Make run timeout
        //TODO: Make running error handling
        private Task RunAsync(string instanceName)
        {
            throw isolatedExecutor.RunAsync(instanceName)
        }
    }
}
