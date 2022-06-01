namespace CodeInTasks.Application.Abstractions.Dtos.Solution
{
    public class SolutionStatusUpdateDto
    {
        public Guid Id { get; set; }

        public TaskSolutionStatus Status { get; set; }
        public TaskSolutionResult? Result { get; set; }
        public string ResultAdditionalInfo { get; set; }

        public DateTime? FinishTime { get; set; }
        public int? RunTimeMs { get; set; }
    }
}
