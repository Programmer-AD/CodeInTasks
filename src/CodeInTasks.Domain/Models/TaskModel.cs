namespace CodeInTasks.Domain.Models
{
    public class TaskModel : ModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public RunnerType Runner { get; set; }
        public string BaseRepositoryName { get; set; }
        public string TestRepositoryName { get; set; }

        public Guid CreatorId { get; set; }
        public UserData Creator { get; set; }
    }
}
