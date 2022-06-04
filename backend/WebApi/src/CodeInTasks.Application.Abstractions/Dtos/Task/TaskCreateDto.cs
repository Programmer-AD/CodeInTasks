namespace CodeInTasks.Application.Abstractions.Dtos.Task
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }

        public RunnerType Runner { get; set; }
        public string BaseRepositoryUrl { get; set; }
        public string TestRepositoryUrl { get; set; }
        public string TestRepositoryAuthPassword { get; set; }

        public Guid CreatorId { get; set; }
    }
}
