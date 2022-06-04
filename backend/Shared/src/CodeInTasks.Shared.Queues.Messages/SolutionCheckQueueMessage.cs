namespace CodeInTasks.Shared.Queues.Messages
{
    public class SolutionCheckQueueMessage
    {
        public Guid SolutionId { get; set; }
        public RunnerType Runner { get; set; }

        public RepositoryInfo TestRepositoryInfo { get; set; }
        public RepositoryInfo SolutionRepositoryInfo { get; set; }


        public record struct RepositoryInfo(string RepositoryUrl, string AuthUserName, string AuthPassword);
    }
}
