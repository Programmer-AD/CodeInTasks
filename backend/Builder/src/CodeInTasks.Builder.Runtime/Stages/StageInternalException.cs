namespace CodeInTasks.Builder.Runtime.Stages
{
    internal class StageInternalException : Exception
    {
        public StageInternalException(string errorCode) : base(errorCode)
        {
            StageErrorCode = errorCode;
        }

        public string StageErrorCode { get; }
    }
}
