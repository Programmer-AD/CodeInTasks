namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal record struct ProcessRunnerArguments(
        string FileName,
        string Arguments,
        TimeSpan Timeout)
    {
        public string WorkingDirectory = null;
    };
}
