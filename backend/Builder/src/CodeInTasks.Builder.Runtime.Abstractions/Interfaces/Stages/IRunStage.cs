namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Stages
{
    public interface IRunStage
    {
        Task<RunStageResult> RunAsync(RunStageArguments stageArguments);

        Task CleanAsync(RunStageArguments stageArguments);
    }
}
