namespace CodeInTasks.Builder.Infrastructure.Docker
{
    public class ProcessRunnerResult
    {
        public int ExitCode { get; set; }
        public bool IsKilled { get; set; }
        public TimeSpan RunTime { get; set; }

        public string StreamOutputText { get; set; }

        public bool HasSuccess => !IsKilled && ExitCode == 0;
    }
}
