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
        [MinLength(DomainConstants.RepositoryName_MinLength)]
        [MaxLength(DomainConstants.RepositoryName_MaxLength)]
        public string BaseRepositoryName { get; set; }

        [Required]
        [MinLength(DomainConstants.RepositoryName_MinLength)]
        [MaxLength(DomainConstants.RepositoryName_MaxLength)]
        public string TestRepositoryName { get; set; }
    }
}
