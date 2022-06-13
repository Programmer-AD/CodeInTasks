namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Arguments
{
    public record struct BuildStageArguments(
        string FolderPath,
        RunnerType Runner,
        string InstanceName);
}
