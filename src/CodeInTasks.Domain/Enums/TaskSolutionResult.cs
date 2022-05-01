namespace CodeInTasks.Domain.Enums
{
    public enum TaskSolutionResult : byte
    {
        DownloadError,
        BuildError,
        RunError,

        TimeLimitExceed,
        Failed,
        Completed
    }
}
