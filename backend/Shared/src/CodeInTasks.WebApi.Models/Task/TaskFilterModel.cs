namespace CodeInTasks.WebApi.Models.Task
{
    public class TaskFilterModel
    {
        [Required]
        public IEnumerable<TaskCategory> Categories { get; set; }

        [Required]
        public IEnumerable<RunnerType> Runners { get; set; }

        public Guid? CreatorId { get; set; }

        [Range(0, int.MaxValue)]
        public int TakeOffset { get; set; }

        [Range(1, ModelConstants.TaskFilter_TakeCount_Max)]
        public int TakeCount { get; set; }
    }
}
