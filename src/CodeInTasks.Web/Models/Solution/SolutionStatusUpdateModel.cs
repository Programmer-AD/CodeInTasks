using System.ComponentModel.DataAnnotations;

namespace CodeInTasks.Web.Models.Solution
{
    public class SolutionStatusUpdateModel
    {
        public Guid Id { get; set; }

        public TaskSolutionStatus Status { get; set; }
        public TaskSolutionResult? Result { get; set; }

        [Required]
        [MinLength(DomainConstants.Solution_ResultAdditionalInfo_MinLength)]
        [MaxLength(DomainConstants.Solution_ResultAdditionalInfo_MaxLength)]
        public string ResultAdditionalInfo { get; set; }

        public DateTime? FinishTime { get; set; }
        public int? RunTimeMs { get; set; }
    }
}
