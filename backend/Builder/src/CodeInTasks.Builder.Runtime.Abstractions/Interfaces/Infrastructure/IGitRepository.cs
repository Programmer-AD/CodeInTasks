namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IGitRepository
    {
        string Path { get; }

        Task CloneAsync(string sourceUrl, GitAuthCredintials gitAuth, CancellationToken cancellationToken);
        Task PullAsync(string sourceUrl, GitAuthCredintials gitAuth, CancellationToken cancellationToken);

        string GetLastCommitId();
        void CheckoutPaths(string commitId, IEnumerable<string> paths);
    }
}
