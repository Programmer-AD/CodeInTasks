namespace CodeInTasks.Application.Abstractions.Dtos.Solution
{
    public class SolutionFilterDto
    {
        public IEnumerable<TaskSolutionResult?> Results { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? SenderId { get; set; }

        public int TakeOffset { get; set; }
        public int TakeCount { get; set; }
    }
}
