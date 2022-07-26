namespace CodeInTasks.Application.Abstractions.Dtos.User
{
    public class UserSignInResultDto
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserData User { get; set; }
    }
}
