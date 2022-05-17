namespace CodeInTasks.Web.Models.User
{
    public class UserSignInModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
