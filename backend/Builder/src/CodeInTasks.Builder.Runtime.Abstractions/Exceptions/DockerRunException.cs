namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public class DockerRunException : DockerException
    {
        private const string actionName = "run";

        public DockerRunException()
            : base(actionName)
        {
        }

        public DockerRunException(string outputText)
            : base(actionName, outputText)
        {
        }
    }
}
