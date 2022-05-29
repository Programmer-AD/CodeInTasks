namespace CodeInTasks.Web.Models.User
{
    public class UserSignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(DomainConstants.User_Password_MinLength)]
        [MaxLength(DomainConstants.User_Password_MaxLength)]
        public string Password { get; set; }
    }
}
