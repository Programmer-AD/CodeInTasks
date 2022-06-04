namespace CodeInTasks.Application.Abstractions.Dtos.Solution
{
    public class SolutionQueueDto
    {
        public Guid Id { get; set; }
        public string RepositoryUrl { get; set; }
        public string RepositoryAuthPassword { get; set; }

        public Guid TaskId { get; set; }
        public Guid SenderId { get; set; }
    }
}
