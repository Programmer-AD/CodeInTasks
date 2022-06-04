namespace CodeInTasks.Builder.Infrastructure.Git
{
    internal class GitRepositoryFactory : IGitRepositoryFactory
    {
        public IGitRepository GetRepository(string path)
        {
            return new GitRepository(path);
        }
    }
}
