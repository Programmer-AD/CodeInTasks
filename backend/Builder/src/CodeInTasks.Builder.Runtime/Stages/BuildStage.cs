namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class BuildStage : StageBase<BuildStageArguments, BuildStageResult>, IBuildStage
    {
        private readonly IIsolatedExecutor isolatedExecutor;

        public BuildStage(IIsolatedExecutor isolatedExecutor)
        {
            this.isolatedExecutor = isolatedExecutor;
        }

        protected override async Task<BuildStageResult> GetResultAsync(BuildStageArguments stageArguments)
        {
            await isolatedExecutor.BuildAsync(stageArguments.FolderPath, stageArguments.Runner, stageArguments.InstanceName, CancellationToken.None);

            //TODO: Add build timeout
            //TODO: Add build exception handling
            //TODO: Add result return
        }

        protected override Task CleanAsync(BuildStageArguments stageArguments)
        {
            return isolatedExecutor.RemoveBuildAsync(stageArguments.InstanceName);
        }
    }
}
