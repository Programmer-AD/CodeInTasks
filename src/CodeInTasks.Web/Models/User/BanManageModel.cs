namespace CodeInTasks.Web.Models.User
{
    public class BanManageModel
    {
        [Required]
        public string Username { get; set; }

        public bool IsBanned { get; set; }
    }
}
