using CodeInTasks.Shared.Queues.Messages;
using CodeInTasks.Shared.Wrappers.Interfaces;

namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class DownloadStage : StageBase<DownloadStageArguments, DownloadStageResult>, IDownloadStage
    {
        private readonly IGitRepositoryFactory gitRepositoryFactory;
        private readonly IFileSystem fileSystem;

        public DownloadStage(IGitRepositoryFactory gitRepositoryFactory, IFileSystem fileSystem)
        {
            this.gitRepositoryFactory = gitRepositoryFactory;
            this.fileSystem = fileSystem;
        }

        protected override async Task<DownloadStageResult> GetResultAsync(DownloadStageArguments stageArguments)
        {
            var gitRepository = gitRepositoryFactory.GetRepository(stageArguments.DestinationFolder);

            await CloneRepositoryAsync(gitRepository, stageArguments.TestRepositoryInfo);

            var lastTestCommitId = gitRepository.GetLastCommitId();

            await PullRepositoryAsync(gitRepository, stageArguments.SolutionRepositoryInfo);

            //TODO: Add configuration stage
            var allFilesPaths = new[] { "*" };
            gitRepository.CheckoutPaths(lastTestCommitId, allFilesPaths);

            //TODO: Add download exception handling
            //TODO: Add result return
        }

        protected override Task CleanAsync(DownloadStageArguments stageArguments)
        {
            fileSystem.DeleteDirectory(stageArguments.DestinationFolder, recursive: true);

            return Task.CompletedTask;
        }

        private static Task CloneRepositoryAsync(IGitRepository gitRepository, RepositoryInfo repositoryInfo)
        {
            var repositoryUrl = repositoryInfo.RepositoryUrl;
            var repositoryAuth = new GitAuthCredintials(repositoryInfo.AuthUserName, repositoryInfo.AuthPassword);

            return gitRepository.CloneAsync(repositoryUrl, repositoryAuth, RuntimeConstants.Git_MaxDownloadSizeBytes);
        }

        private static Task PullRepositoryAsync(IGitRepository gitRepository, RepositoryInfo repositoryInfo)
        {
            var repositoryUrl = repositoryInfo.RepositoryUrl;
            var repositoryAuth = new GitAuthCredintials(repositoryInfo.AuthUserName, repositoryInfo.AuthPassword);

            return gitRepository.PullAsync(repositoryUrl, repositoryAuth, RuntimeConstants.Git_MaxDownloadSizeBytes);
        }
    }
}
