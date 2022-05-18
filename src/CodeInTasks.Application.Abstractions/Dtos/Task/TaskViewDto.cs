namespace CodeInTasks.Application.Abstractions.Dtos.Task
{
    public class TaskViewDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }
        public DateTime CreateDate { get; set; }

        public RunnerType Runner { get; set; }
        public string BaseRepositoryName { get; set; }
        public string TestRepositoryName { get; set; }

        public Guid CreatorId { get; set; }

    }
}
