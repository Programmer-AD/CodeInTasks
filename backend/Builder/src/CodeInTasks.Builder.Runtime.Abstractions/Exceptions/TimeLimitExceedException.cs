namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public class TimeLimitExceedException : Exception
    {
        private const string CustomMessage = "Time limit exceed!";

        public TimeLimitExceedException()
            : base(CustomMessage)
        {
        }
    }
}
