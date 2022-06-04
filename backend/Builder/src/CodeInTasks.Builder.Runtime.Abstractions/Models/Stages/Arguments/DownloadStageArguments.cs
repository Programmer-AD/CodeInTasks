namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Arguments
{
    public class DownloadStageArguments
    {
        public string DestinationFolder { get; set; }
        public RepositoryInfo TestRepositoryInfo { get; set; }
        public RepositoryInfo SolutionRepositoryInfo { get; set; }
    }
}
