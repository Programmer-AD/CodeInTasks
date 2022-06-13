namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Arguments
{
    public record struct DownloadStageArguments(
        string DestinationFolder,
        RepositoryInfo TestRepositoryInfo,
        RepositoryInfo SolutionRepositoryInfo);
}
