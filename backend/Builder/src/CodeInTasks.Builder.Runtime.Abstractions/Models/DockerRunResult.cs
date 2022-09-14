namespace CodeInTasks.Builder.Runtime.Abstractions.Models
{
    public class DockerRunResult
    {
        public bool HasSuccessExitCode { get; init; }
        public TimeSpan RunTime { get; init; }
        public string StreamOutputText { get; init; }
    }
}
