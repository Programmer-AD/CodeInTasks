namespace CodeInTasks.Domain.Models
{
    public class Solution : ModelBase
    {
        public string RepositoryUrl { get; set; }
        public string RepositoryAuthPassword { get; set; }

        public TaskSolutionStatus Status { get; set; } = TaskSolutionStatus.Queued;
        public TaskSolutionResult? Result { get; set; }
        public string ErrorCode { get; set; }
        public string ResultAdditionalInfo { get; set; }

        public DateTime SendTime { get; set; } = DateTime.UtcNow;
        public DateTime? FinishTime { get; set; }
        public int? RunTimeMs { get; set; }

        public Guid TaskId { get; set; }
        public TaskModel Task { get; set; }

        public Guid SenderId { get; set; }
        public UserData Sender { get; set; }
    }
}
