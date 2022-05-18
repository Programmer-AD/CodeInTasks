namespace CodeInTasks.Application.Abstractions.Exceptions
{
    [Serializable]
    public class IdentityException : Exception
    {
        public IdentityException() { }

        public IdentityException(IEnumerable<string> errors)
            : base(GetMessageByErrors(errors))
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }

        private static string GetMessageByErrors(IEnumerable<string> errors)
        {
            var result = string.Join(';', errors);
            return result;
        }
    }
}
