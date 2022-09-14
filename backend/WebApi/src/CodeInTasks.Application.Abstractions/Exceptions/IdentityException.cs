namespace CodeInTasks.Application.Abstractions.Exceptions
{
    [Serializable]
    public class IdentityException : Exception
    {
        public IdentityException() { }

        public IdentityException(IEnumerable<string> errors)
            : base(GetMessage(errors))
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }

        private static string GetMessage(IEnumerable<string> errors)
        {
            var result = string.Join(';', errors);
            return result;
        }
    }
}
