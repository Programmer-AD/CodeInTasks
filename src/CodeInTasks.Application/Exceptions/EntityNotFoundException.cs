using System.Runtime.Serialization;

namespace CodeInTasks.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string message)
            : base(message) { }
    }
}
