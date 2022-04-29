namespace CodeInTasks.Domain.Enums
{
    [Flags]
    public enum TaskCategory : int
    {
        Implement = 1 << 0,
        AddPart = 1 << 1,
        FixBugs = 1 << 2,
        AddTests = 1 << 3,
    }
}
