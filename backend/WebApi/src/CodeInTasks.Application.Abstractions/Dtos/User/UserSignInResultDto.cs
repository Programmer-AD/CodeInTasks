namespace CodeInTasks.Application.Abstractions.Dtos.User
{
    public class UserSignInResultDto
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }

        public bool IsSucceeded => !string.IsNullOrEmpty(Token);
    }
}
