using Microsoft.AspNetCore.Identity;

namespace CodeInTasks.Application.Exceptions
{
    [Serializable]
    public class IdentityException : Exception
    {
        public IdentityException() { }

        public IdentityException(IEnumerable<IdentityError> errors)
            : base(GetMessageByErrors(errors))
        {
            Errors = errors;
        }

        public IEnumerable<IdentityError> Errors { get; }

        private static string GetMessageByErrors(IEnumerable<IdentityError> errors)
        {
            var errorTexts = errors.Select(x => $"{x.Code}: {x.Description}");
            var result = string.Join(';', errorTexts);
            return result;
        }
    }
}
