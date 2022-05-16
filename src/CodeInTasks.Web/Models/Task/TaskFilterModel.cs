namespace CodeInTasks.Web.Models.Task
{
    public class TaskFilterModel
    {
        [Required]
        public IEnumerable<TaskCategory> Categories { get; set; }

        [Required]
        public IEnumerable<RunnerType> Runners { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
