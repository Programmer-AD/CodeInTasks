namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Arguments
{
    public record struct ConfigurationStageArguments(
        string FolderPath,
        RunnerType RunnerType,
        string LastTestRepositoryCommitId);
}
