namespace CodeInTasks.Web.Models.Solution
{
    public class SolutionCreateModel
    {
        [Required]
        [MinLength(DomainConstants.TaskModel_Description_MinLength)]
        [MaxLength(DomainConstants.TaskModel_Title_MaxLength)]
        public string RepositoryName { get; set; }

        public Guid TaskId { get; set; }
    }
}
