namespace CodeInTasks.Application.Abstractions.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string message)
            : base(message) { }

        public EntityNotFoundException(string enityTypeName, Guid id)
            : base(GetMessage(enityTypeName, id)) { }

        private static string GetMessage(string enityTypeName, Guid id)
        {
            return $"Not found {enityTypeName} width Id=\"{id}\"";
        }
    }
}
