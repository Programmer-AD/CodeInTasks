namespace CodeInTasks.Shared.Queues.Messages
{
    public record struct RepositoryInfo(
        string RepositoryUrl,
        string AuthUserName,
        string AuthPassword);
}
