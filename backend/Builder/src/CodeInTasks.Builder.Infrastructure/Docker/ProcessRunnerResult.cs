namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal class ProcessRunnerResult
    {
        public int ExitCode { get; set; }
        public bool IsKilled { get; set; }
        public TimeSpan RunTime { get; set; }

        public string OutputStreamText { get; set; }
        public string ErrorStreamText { get; set; }

        public bool HasSuccess => !IsKilled && ExitCode == 0;
    }
}
