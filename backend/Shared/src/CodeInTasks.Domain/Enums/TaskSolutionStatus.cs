namespace CodeInTasks.Domain.Enums
{
    public enum TaskSolutionStatus : byte
    {
        Queued,
        Downloading,
        Building,
        Running,
        Finished
    }
}
