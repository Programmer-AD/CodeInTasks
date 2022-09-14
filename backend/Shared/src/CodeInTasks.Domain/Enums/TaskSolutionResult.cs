namespace CodeInTasks.Domain.Enums
{
    public enum TaskSolutionResult : byte
    {
        Unknown,

        DownloadError,
        BuildError,
        RunError,

        Failed,
        Completed
    }
}
