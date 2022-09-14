namespace CodeInTasks.Builder.Runtime.Abstractions.Exceptions
{
    public abstract class DockerException : Exception
    {
        private const string CustomMessageFormat = "Docker exception at \"{0}\"! Output is:{1}";

        protected DockerException(string actionName)
            : base(GetMessageWithOutput(actionName, string.Empty))
        {
        }

        protected DockerException(string actionName, string outputText)
            : base(GetMessageWithOutput(actionName, outputText))
        {
            OutputText = outputText;
        }

        public string OutputText { get; }

        private static string GetMessageWithOutput(string actionName, string outputText)
        {
            return string.Format(CustomMessageFormat, actionName, outputText);
        }
    }
}
