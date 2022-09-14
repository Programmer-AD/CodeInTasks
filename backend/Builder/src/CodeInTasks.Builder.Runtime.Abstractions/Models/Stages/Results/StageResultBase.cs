namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public abstract class StageResultBase
    {
        protected StageResultBase(bool isSucceded, string errorCode = null, string additionalInfo = null) 
        {
            IsSucceded = isSucceded;
            ErrorCode = errorCode;
            AdditionalInfo = additionalInfo;
        }

        public bool IsSucceded { get; }
        public string ErrorCode { get; }
        public string AdditionalInfo { get; }
    }
}
