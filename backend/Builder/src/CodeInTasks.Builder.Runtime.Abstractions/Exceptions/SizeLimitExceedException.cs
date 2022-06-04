namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public class SizeLimitExceedException : Exception
    {
        private const string CustomMessage = "Size limit exceed exception!";

        public SizeLimitExceedException()
            : base(CustomMessage)
        {
        }
    }
}
