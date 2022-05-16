namespace CodeInTasks.Web.Models.Solution
{
    public class SolutionFilterModel
    {
        [Required]
        public IEnumerable<TaskSolutionResult> Results { get; set; }

        public Guid? TaskId { get; set; }
    }
}
