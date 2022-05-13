namespace CodeInTasks.Web.Models.Solution
{
    public class SolutionStatusUpdateModel
    {
        public Guid Id { get; set; }

        public TaskSolutionStatus Status { get; set; }
        public TaskSolutionResult? Result { get; set; }
        public string ResultAdditionalInfo { get; set; }

        public DateTime? FinishTime { get; set; }
        public int? RunTimeMs { get; set; }
    }
}
