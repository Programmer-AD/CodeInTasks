namespace CodeInTasks.Web.Models.Task
{
    public class TaskUpdateModel
    {
        [Required]
        [MinLength(DomainConstants.TaskModel_Title_MinLength)]
        [MaxLength(DomainConstants.TaskModel_Title_MaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DomainConstants.TaskModel_Description_MinLength)]
        [MaxLength(DomainConstants.TaskModel_Description_MaxLength)]
        public string Description { get; set; }

        public TaskCategory Category { get; set; }

        public RunnerType Runner { get; set; }

        [Required]
        [MinLength(DomainConstants.RepositoryUrl_MinLength)]
        [MaxLength(DomainConstants.RepositoryUrl_MaxLength)]
        public string BaseRepositoryUrl { get; set; }

        [Required]
        [MinLength(DomainConstants.RepositoryUrl_MinLength)]
        [MaxLength(DomainConstants.RepositoryUrl_MaxLength)]
        public string TestRepositoryUrl { get; set; }

        [Required]
        [MinLength(DomainConstants.RepositoryAccessToken_MinLength)]
        [MaxLength(DomainConstants.RepositoryAccessToken_MaxLength)]
        public string TestRepositoryAccessToken { get; set; }
    }
}
