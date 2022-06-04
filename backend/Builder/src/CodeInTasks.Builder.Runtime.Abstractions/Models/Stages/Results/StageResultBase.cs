namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public abstract class StageResultBase
    {
        public bool IsSucceded { get; set; }

        public string ErrorCode { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
