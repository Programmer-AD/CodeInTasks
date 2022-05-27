namespace CodeInTasks.Web.Models.Solution
{
    public class SolutionViewModel
    {
        public Guid Id { get; set; }
        public string RepositoryUrl { get; set; }

        public TaskSolutionStatus Status { get; set; }
        public TaskSolutionResult? Result { get; set; }
        public string ResultAdditionalInfo { get; set; }

        public DateTime SendTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public int? RunTimeMs { get; set; }

        public Guid TaskId { get; set; }
        public Guid SenderId { get; set; }
    }
}
