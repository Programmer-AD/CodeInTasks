using CodeInTasks.Shared.Wrappers.Interfaces;

namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class ConfigurationStage : StageBase<ConfigurationStageArguments, ConfigurationStageResult>, IConfigurationStage
    {
        private static readonly IEnumerable<string> allFilesPaths = new[] { "*" };

        private readonly IGitRepositoryFactory gitRepositoryFactory;
        private readonly IFileSystem fileSystem;
        private readonly IJsonSerializer jsonSerializer;

        public ConfigurationStage(
            IGitRepositoryFactory gitRepositoryFactory,
            IFileSystem fileSystem,
            IJsonSerializer jsonSerializer)
        {
            this.gitRepositoryFactory = gitRepositoryFactory;
            this.fileSystem = fileSystem;
            this.jsonSerializer = jsonSerializer;
        }

        protected override async Task<ConfigurationStageResult> GetResultAsync(ConfigurationStageArguments arguments)
        {
            var gitRepository = gitRepositoryFactory.GetRepository(arguments.FolderPath);

            try
            {
                CheckoutAllFilesToTrusted(gitRepository, arguments.LastTestRepositoryCommitId);

                var config = await GetTaskConfig(arguments.FolderPath);

                CheckoutSolutionFiles(gitRepository, config);

                //TODO: make copying of Dockerfile and other files due to runner

                RemoveGitFolder(arguments.FolderPath);

                return new(isSucceded: true);
            }
            catch (StageInternalException stageException)
            {
                return new(isSucceded: false, stageException.StageErrorCode);
            }
        }

        protected override Task CleanAsync(ConfigurationStageArguments arguments)
        {
            return Task.CompletedTask;
        }

        private static void CheckoutAllFilesToTrusted(IGitRepository gitRepository, string lastTestRepositoryCommitId)
        {
            gitRepository.CheckoutPaths(lastTestRepositoryCommitId, allFilesPaths);
        }

        private async Task<TaskConfig> GetTaskConfig(string folderPath)
        {
            var configPath = Path.Combine(folderPath, RuntimeConstants.TaskConfig_FileName);

            if (!fileSystem.IsFileExists(configPath))
            {
                throw new StageInternalException(ErrorCodes.Build_Configuration_NoConfig);
            }

            using var configStream = fileSystem.OpenRead(configPath);

            if (configStream.Length > RuntimeConstants.TaskConfig_MaxSizeBytes)
            {
                throw new StageInternalException(ErrorCodes.Build_Configuration_TooLargeConfig);
            }

            using var configReader = new StreamReader(configStream);

            var configData = await configReader.ReadToEndAsync();

            if (!jsonSerializer.TryDeserialize(configData, out TaskConfig config))
            {
                throw new StageInternalException(ErrorCodes.Build_Configuration_WrongConfig);
            }

            return config;
        }

        private static void CheckoutSolutionFiles(IGitRepository gitRepository, TaskConfig config)
        {
            var lastCommitId = gitRepository.GetLastCommitId();
            gitRepository.CheckoutPaths(lastCommitId, config.SolutionPaths);
        }

        private void RemoveGitFolder(string folderPath)
        {
            var gitFolder = Path.Combine(folderPath, RuntimeConstants.Git_FolderName);

            fileSystem.DeleteDirectory(gitFolder, recursive: true);
        }

    }
}
