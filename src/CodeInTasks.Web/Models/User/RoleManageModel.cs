namespace CodeInTasks.Web.Models.User
{
    public class RoleManageModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public bool IsSetted { get; set; }
    }
}
