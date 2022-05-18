namespace CodeInTasks.Web.Models.User
{
    public class UserSignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(IdentityConstants.Password_RequiredLength)]
        public string Password { get; set; }
    }
}
