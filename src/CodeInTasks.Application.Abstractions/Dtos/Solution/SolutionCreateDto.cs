namespace CodeInTasks.Application.Abstractions.Dtos.Solution
{
    public class SolutionCreateDto
    {
        public string RepositoryName { get; set; }
        public Guid TaskId { get; set; }
        public Guid SenderId { get; set; }
    }
}
