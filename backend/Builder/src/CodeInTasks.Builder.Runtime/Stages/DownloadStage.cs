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

            var cloneErrorCode = await TryRepositoryDownloadAsync(gitRepository.CloneAsync, stageArguments.TestRepositoryInfo);

            if (!string.IsNullOrEmpty(cloneErrorCode))
            {
                var result = new DownloadStageResult(
                    isSucceded: false,
                    cloneErrorCode,
                    ErrorCodes.Download_ErrorAdditionalInfo_TestRepository);

                return result;
            }

            var lastTestRepositoryCommitId = gitRepository.GetLastCommitId();

            var pullErrorCode = await TryRepositoryDownloadAsync(gitRepository.PullAsync, stageArguments.TestRepositoryInfo);

            if (!string.IsNullOrEmpty(pullErrorCode))
            {
                var result = new DownloadStageResult(
                    isSucceded: false,
                    pullErrorCode,
                    ErrorCodes.Download_ErrorAdditionalInfo_SolutionRepository);

                return result;
            }

            var successResult = new DownloadStageResult(isSucceded: true)
            {
                LastTestRepositoryCommitID = lastTestRepositoryCommitId,
            };
            return successResult;
        }

        protected override Task CleanAsync(DownloadStageArguments stageArguments)
        {
            fileSystem.DeleteDirectory(stageArguments.DestinationFolder, recursive: true);

            return Task.CompletedTask;
        }

        private static async Task<string> TryRepositoryDownloadAsync(
            Func<string, GitAuthCredintials, long, Task> downloadFunc,
            RepositoryInfo repositoryInfo)
        {
            var repositoryUrl = repositoryInfo.RepositoryUrl;
            var repositoryAuth = new GitAuthCredintials(repositoryInfo.AuthUserName, repositoryInfo.AuthPassword);

            try
            {
                await downloadFunc(repositoryUrl, repositoryAuth, RuntimeConstants.Download_MaxSizeBytes);

                return null;
            }
            catch (MemoryLimitExceedException)
            {
                return ErrorCodes.Download_MemoryLimitExceed;
            }
            catch (Exception)
            {
                return ErrorCodes.Download_Error;
            }
        }
    }
}
