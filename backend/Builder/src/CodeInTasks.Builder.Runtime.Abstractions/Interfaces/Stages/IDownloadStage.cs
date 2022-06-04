namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Stages
{
    public interface IDownloadStage
    {
        Task<DownloadStageResult> DownloadAsync(DownloadStageArguments stageArguments);

        Task CleanAsync(DownloadStageArguments stageArguments);
    }
}
