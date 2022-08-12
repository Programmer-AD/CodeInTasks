namespace CodeInTasks.WebApi.Models.Solution
{
    public class SolutionFilterModel
    {
        [Required]
        public IEnumerable<TaskSolutionResult?> Results { get; set; }

        public Guid? TaskId { get; set; }

        public Guid? SenderId { get; set; }

        [Range(0, int.MaxValue)]
        public int TakeOffset { get; set; }

        [Range(1, ModelConstants.SolutionFilter_TakeCount_Max)]
        public int TakeCount { get; set; }
    }
}
