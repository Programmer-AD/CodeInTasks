namespace CodeInTasks.Web.Models.User
{
    public class RoleManageModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string RoleName { get; set; }

        public bool IsSetted { get; set; }
    }
}
