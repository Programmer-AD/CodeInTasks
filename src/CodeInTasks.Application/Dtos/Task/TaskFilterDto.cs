namespace CodeInTasks.Application.Dtos.Task
{
    public class TaskFilterDto
    {
        public IEnumerable<TaskCategory> Categories { get; set; }
        public IEnumerable<RunnerType> Runners { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
