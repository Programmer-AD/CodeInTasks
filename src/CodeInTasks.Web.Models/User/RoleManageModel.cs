using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Web.Models.User
{
    public class RoleManageModel
    {
        public Guid UserId { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsSetted { get; set; }
    }
}
