namespace CodeInTasks.Application.Abstractions.Dtos.Task
{
    public class TaskFilterDto
    {
        public IEnumerable<TaskCategory> Categories { get; set; }
        public IEnumerable<RunnerType> Runners { get; set; }
        public Guid? CreatorId { get; set; }

        public int TakeOffset { get; set; }
        public int TakeCount { get; set; }
    }
}
