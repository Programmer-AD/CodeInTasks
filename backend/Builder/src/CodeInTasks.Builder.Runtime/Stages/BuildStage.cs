namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class BuildStage : StageBase<BuildStageArguments, BuildStageResult>, IBuildStage
    {
        private readonly IDockerProvider isolatedExecutor;

        public BuildStage(IDockerProvider isolatedExecutor)
        {
            this.isolatedExecutor = isolatedExecutor;
        }

        protected override async Task<BuildStageResult> GetResultAsync(BuildStageArguments stageArguments)
        {
            try
            {
                await isolatedExecutor.BuildAsync(stageArguments.FolderPath, stageArguments.InstanceName, RuntimeConstants.BuildTimeout);

                var result = new BuildStageResult(isSucceded: true);

                return result;
            }
            catch (TimeoutException)
            {
                var result = new BuildStageResult(isSucceded: false, ErrorCodes.Build_Timeout);

                return result;
            }
            catch (DockerBuildException)
            {
                var result = new BuildStageResult(isSucceded: false, ErrorCodes.Build_Error);

                return result;
            }
        }

        protected override Task CleanAsync(BuildStageArguments stageArguments)
        {
            return isolatedExecutor.RemoveImageAsync(stageArguments.InstanceName);
        }
    }
}
