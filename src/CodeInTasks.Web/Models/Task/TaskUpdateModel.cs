namespace CodeInTasks.Web.Models.Task
{
    public class TaskUpdateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskCategory Category { get; set; }

        public RunnerType Runner { get; set; }
        public string BaseRepositoryName { get; set; }
        public string TestRepositoryName { get; set; }
    }
}
