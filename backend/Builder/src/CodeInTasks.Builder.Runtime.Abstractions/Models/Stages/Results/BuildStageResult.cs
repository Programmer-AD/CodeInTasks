namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public class BuildStageResult : StageResultBase
    {
        public BuildStageResult(bool isSucceded, string errorCode = null, string additionalInfo = null)
            : base(isSucceded, errorCode, additionalInfo)
        {
        }
    }
}
