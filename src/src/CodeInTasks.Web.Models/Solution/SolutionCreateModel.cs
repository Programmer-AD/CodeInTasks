namespace CodeInTasks.Web.Models.Solution
{
    public class SolutionCreateModel
    {
        [Required]
        [MinLength(DomainConstants.RepositoryUrl_MinLength)]
        [MaxLength(DomainConstants.RepositoryUrl_MaxLength)]
        public string RepositoryUrl { get; set; }

        [Required]
        [MinLength(DomainConstants.RepositoryAccessToken_MinLength)]
        [MaxLength(DomainConstants.RepositoryAccessToken_MaxLength)]
        public string RepositoryAccessToken { get; set; }

        public Guid TaskId { get; set; }
    }
}
