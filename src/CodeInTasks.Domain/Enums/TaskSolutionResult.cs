namespace CodeInTasks.Domain.Enums
{
    public enum TaskSolutionResult : byte
    {
        Unknown,

        DownloadError,
        BuildError,
        RunError,

        TimeLimitExceed,
        Failed,
        Completed
    }
}
