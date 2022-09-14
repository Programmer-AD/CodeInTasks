namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class BuildStage : StageBase<BuildStageArguments, BuildStageResult>, IBuildStage
    {
        private readonly IDockerProvider dockerProvider;

        public BuildStage(IDockerProvider dockerProvider)
        {
            this.dockerProvider = dockerProvider;
        }

        protected override async Task<BuildStageResult> GetResultAsync(BuildStageArguments stageArguments)
        {
            try
            {
                await dockerProvider.BuildAsync(stageArguments.FolderPath, stageArguments.InstanceName, RuntimeConstants.BuildTimeout);

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
            return dockerProvider.RemoveImageAsync(stageArguments.InstanceName);
        }
    }
}
