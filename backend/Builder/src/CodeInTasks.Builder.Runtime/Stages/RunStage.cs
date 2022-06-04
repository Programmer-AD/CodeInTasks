namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class RunStage : StageBase<RunStageArguments, RunStageResult>, IRunStage
    {
        private readonly IIsolatedExecutor isolatedExecutor;

        public RunStage(IIsolatedExecutor isolatedExecutor)
        {
            this.isolatedExecutor = isolatedExecutor;
        }

        protected override async Task<RunStageResult> GetResultAsync(RunStageArguments stageArguments)
        {
            await isolatedExecutor.RunAsync(stageArguments.InstanceName, stageArguments.Runner, CancellationToken.None);

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
