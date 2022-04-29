namespace CodeInTasks.Domain.Enums
{
    public enum TaskSolutionResult : byte
    {
        DownloadError,
        BuildError,
        RunError,

        Failed,
        Completed
    }
}
