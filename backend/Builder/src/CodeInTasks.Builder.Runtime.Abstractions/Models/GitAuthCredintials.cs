namespace CodeInTasks.Builder.Runtime.Abstractions.Models
{
    public class GitAuthCredintials
    {
        public GitAuthCredintials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
