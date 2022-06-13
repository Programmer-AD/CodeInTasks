namespace CodeInTasks.Builder.Runtime.Abstractions.Models
{
    public class DockerRunResult
    {
        public bool HasSuccessExitCode { get; set; }
        public TimeSpan RunTime { get; set; }
        public string StreamOutputText { get; set; }
    }
}
