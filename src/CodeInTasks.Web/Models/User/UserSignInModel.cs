namespace CodeInTasks.Web.Models.User
{
    public class UserSignInModel
    {
        [Required]
        public string Username { get; set; }

        //TODO: Paasword length limitation
        [Required]
        public string Password { get; set; }
    }
}
