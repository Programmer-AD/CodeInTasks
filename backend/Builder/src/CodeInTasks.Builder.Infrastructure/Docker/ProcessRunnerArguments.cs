namespace CodeInTasks.Builder.Infrastructure.Docker
{
    public record struct ProcessRunnerArguments(
        string FileName,
        string Arguments,
        TimeSpan Timeout)
    {
        public string WorkingDirectory = null;
    };
}
