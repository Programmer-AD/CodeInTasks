namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public class DockerBuildException : DockerException
    {
        private const string actionName = "build";

        public DockerBuildException()
            : base(actionName)
        {
        }

        public DockerBuildException(string outputText)
            : base(actionName, outputText)
        {
        }
    }
}
