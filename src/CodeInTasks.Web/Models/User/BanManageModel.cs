namespace CodeInTasks.Web.Models.User
{
    public class BanManageModel
    {
        [Required]
        public Guid UserId { get; set; }

        public bool IsBanned { get; set; }
    }
}
