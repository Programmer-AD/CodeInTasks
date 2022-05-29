namespace CodeInTasks.Application.Abstractions.Dtos.Solution
{
    public class SolutionCreateDto
    {
        public string RepositoryUrl { get; set; }
        public string RepositoryAccessToken { get; set; }
        public Guid TaskId { get; set; }
        public Guid SenderId { get; set; }
    }
}
