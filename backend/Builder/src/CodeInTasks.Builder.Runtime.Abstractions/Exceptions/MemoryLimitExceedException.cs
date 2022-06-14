namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public class MemoryLimitExceedException : Exception
    {
        private const string CustomMessage = "Size limit exceed!";

        public MemoryLimitExceedException()
            : base(CustomMessage)
        {
        }
    }
}
