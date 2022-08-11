namespace CodeInTasks.Web.Models.User
{
    public class UserSignInResultModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserViewModel User { get; set; }
    }
}
