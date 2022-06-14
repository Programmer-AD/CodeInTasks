namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class RunStage : StageBase<RunStageArguments, RunStageResult>, IRunStage
    {
        private readonly IDockerProvider dockerProvider;

        public RunStage(IDockerProvider dockerProvider)
        {
            this.dockerProvider = dockerProvider;
        }

        protected override async Task<RunStageResult> GetResultAsync(RunStageArguments stageArguments)
        {
            try
            {
                var runResult = await dockerProvider.RunAsync(stageArguments.InstanceName, RuntimeConstants.RunTimeout);

                var runTimeMs = (int)Math.Floor(runResult.RunTime.TotalMilliseconds);

                var result = new RunStageResult(isSucceded: true)
                {
                    IsTaskCompleted = runResult.HasSuccessExitCode,
                    RunTimeMs = runTimeMs
                };

                return result;
            }
            catch (TimeoutException)
            {
                var result = new RunStageResult(isSucceded: false, ErrorCodes.Run_Timeout);

                return result;
            }
            catch (DockerRunException)
            {
                var result = new RunStageResult(isSucceded: false, ErrorCodes.Run_Error);

                return result;
            }
        }

        protected override Task CleanAsync(RunStageArguments stageArguments)
        {
            return Task.CompletedTask;
        }
    }
}
