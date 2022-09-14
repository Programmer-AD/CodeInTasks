namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IGitRepositoryFactory
    {
        /// <summary>
        /// Creates <see cref="IGitRepository"/> for path = <paramref name="path"/>
        /// <para>
        /// It isn't init repository, just making instance for path
        /// </para>
        /// <para>
        /// Folder will be created, if it not exists. If it already exist is must be empty
        /// </para>
        /// </summary>
        /// <param name="path">Path of folder where repository will exist</param>
        IGitRepository GetRepository(string path);
    }
}
