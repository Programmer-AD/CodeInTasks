namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Stages
{
    public interface IBuildStage
    {
        Task<BuildStageResult> BuildAsync(BuildStageArguments stageArguments);

        Task CleanAsync(BuildStageArguments stageArguments);
    }
}
