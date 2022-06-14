namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IGitRepository
    {
        /// <summary>
        /// Path to repository folder at local system
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Clones repository. This action required before any other
        /// </summary>
        /// <param name="sourceUrl">Remote url from where to clone</param>
        /// <param name="gitAuth">Authorization credentials</param>
        /// <param name="sizeLimitBytes">Max download size in bytes</param>
        /// <exception cref="MemoryLimitExceedException" />
        Task CloneAsync(string sourceUrl, GitAuthCredintials gitAuth, long sizeLimitBytes);

        /// <summary>
        /// Fetch remote repository and merge it using strategy "Ours".
        /// <para>
        /// Throws <see cref="InvalidOperationException"/> if called before clone
        /// </para>
        /// </summary>
        /// <param name="sourceUrl">Remote url from where to fetch</param>
        /// <param name="gitAuth">Authorization credentials</param>
        /// <param name="sizeLimitBytes">Max download size in bytes</param>
        /// <exception cref="InvalidOperationException" />
        /// <exception cref="MemoryLimitExceedException" />
        Task PullAsync(string sourceUrl, GitAuthCredintials gitAuth, long sizeLimitBytes);

        /// <summary>
        /// <para>
        /// Throws <see cref="InvalidOperationException"/> if called before clone
        /// </para>
        /// </summary>
        /// <returns>SHA-Id of last commit</returns>
        /// <exception cref="InvalidOperationException" />
        string GetLastCommitId();

        /// <summary>
        /// Checkouts every file in <paramref name="paths"/> to commit <paramref name="commitId"/>
        /// <para>
        /// Supports "*" as part of paths
        /// </para>
        /// <para>
        /// Throws <see cref="InvalidOperationException"/> if called before clone
        /// </para>
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        void CheckoutPaths(string commitId, IEnumerable<string> paths);
    }
}
