using CodeInTasks.Application.Dtos.User;

namespace CodeInTasks.Web.Models.User
{
    public class RoleManageModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public RoleEnum Role { get; set; }

        public bool IsSetted { get; set; }
    }
}
