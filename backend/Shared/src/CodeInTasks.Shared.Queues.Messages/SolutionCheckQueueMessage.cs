namespace CodeInTasks.Shared.Queues.Messages
{
    public class SolutionCheckQueueMessage
    {
        public Guid SolutionId { get; set; }
        public RunnerType Runner { get; set; }

        public string TestRepositoryUrl { get; set; }
        public string TestRepositoryAccessToken { get; set; }

        public string UserRepositoryUrl { get; set; }
        public string UserRepositoryAccessToken { get; set; }
    }
}
