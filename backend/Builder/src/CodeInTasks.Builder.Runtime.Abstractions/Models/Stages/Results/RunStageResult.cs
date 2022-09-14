namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public class RunStageResult : StageResultBase
    {
        public RunStageResult(bool isSucceded, string errorCode = null, string additionalInfo = null)
            : base(isSucceded, errorCode, additionalInfo)
        {
        }

        public bool IsTaskCompleted { get; init; }
        public int RunTimeMs { get; init; }
    }
}
