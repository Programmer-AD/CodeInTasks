namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class RunStage : StageBase<RunStageArguments, RunStageResult>, IRunStage
    {
        private readonly IDockerProvider isolatedExecutor;

        public RunStage(IDockerProvider isolatedExecutor)
        {
            this.isolatedExecutor = isolatedExecutor;
        }

        protected override async Task<RunStageResult> GetResultAsync(RunStageArguments stageArguments)
        {
            await isolatedExecutor.RunAsync(stageArguments.InstanceName, );

            //TODO: Add run timeout
            //TODO: Add run exception handling
            //TODO: Add result return
        }

        protected override Task CleanAsync(RunStageArguments stageArguments)
        {
            return Task.CompletedTask;
        }
    }
}
