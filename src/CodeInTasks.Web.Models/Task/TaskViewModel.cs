namespace CodeInTasks.Web.Models.Task
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }
        public DateTime CreateDate { get; set; }

        public RunnerType Runner { get; set; }
        public string BaseRepositoryUrl { get; set; }
        public string TestRepositoryUrl { get; set; }

        public Guid CreatorId { get; set; }
    }
}
