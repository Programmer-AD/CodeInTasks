namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public class DownloadStageResult : StageResultBase
    {
        public DownloadStageResult(bool isSucceded, string errorCode = null, string additionalInfo = null)
            : base(isSucceded, errorCode, additionalInfo)
        {
        }

        public string LastTestRepositoryCommitID { get; init; }
    }
}
