namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public class TimeLimitExceedException : Exception
    {
        private const string CustomMessage = "Time limit exceed exception!";

        public TimeLimitExceedException()
            : base(CustomMessage)
        {
        }
    }
}
