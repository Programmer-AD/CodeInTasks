namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public class ConfigurationStageResult : StageResultBase
    {
        public ConfigurationStageResult(bool isSucceded, string errorCode = null, string additionalInfo = null)
            : base(isSucceded, errorCode, additionalInfo)
        {
        }
    }
}
