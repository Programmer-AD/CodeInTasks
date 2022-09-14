namespace CodeInTasks.WebApi.Models.Solution
{
    public class SolutionCreateModel
    {
        [Required]
        [MinLength(DomainConstants.RepositoryUrl_MinLength)]
        [MaxLength(DomainConstants.RepositoryUrl_MaxLength)]
        public string RepositoryUrl { get; set; }

        [Required]
        [MinLength(DomainConstants.RepositoryAuthPassword_MinLength)]
        [MaxLength(DomainConstants.RepositoryAuthPassword_MaxLength)]
        public string RepositoryAuthPassword { get; set; }

        public Guid TaskId { get; set; }
    }
}
