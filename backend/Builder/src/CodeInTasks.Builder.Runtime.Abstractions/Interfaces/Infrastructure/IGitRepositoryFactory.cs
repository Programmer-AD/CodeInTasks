namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IGitRepositoryFactory
    {
        IGitRepository GetRepository(string path);
    }
}
