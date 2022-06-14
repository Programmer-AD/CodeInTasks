using CodeInTasks.Shared.Wrappers.Interfaces;

namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class ConfigurationStage : StageBase<ConfigurationStageArguments, ConfigurationStageResult>, IConfigurationStage
    {
        private readonly IGitRepositoryFactory gitRepositoryFactory;
        private readonly IFileSystem fileSystem;

        public ConfigurationStage(
            IGitRepositoryFactory gitRepositoryFactory,
            IFileSystem fileSystem)
        {
            this.gitRepositoryFactory = gitRepositoryFactory;
            this.fileSystem = fileSystem;
        }

        protected override Task<ConfigurationStageResult> GetResultAsync(ConfigurationStageArguments arguments)
        {
            var gitRepository = gitRepositoryFactory.GetRepository(arguments.FolderPath);

            var solutionRepositoryLastCommitId = gitRepository.GetLastCommitId();

            var allFilesPaths = new[] { "*" };
            gitRepository.CheckoutPaths(arguments.LastTestRepositroyCommitId, allFilesPaths);

            //TODO: make config parsing
            //TODO: make back checkout of solution repository due to config
            //TODO: make copying of Dockerfile and other files due to runner
            //TODO: make .git folder deleting
        }

        protected override Task CleanAsync(ConfigurationStageArguments arguments)
        {
            return Task.CompletedTask;
        }
    }
}
