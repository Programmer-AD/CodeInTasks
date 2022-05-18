namespace CodeInTasks.Application.Abstractions.Dtos.Solution
{
    public class SolutionQueueDto
    {
        public Guid Id { get; set; }
        public string RepositoryName { get; set; }

        public Guid TaskId { get; set; }
        public Guid SenderId { get; set; }
    }
}
