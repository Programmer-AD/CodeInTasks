using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Web.Models.User
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public IEnumerable<RoleEnum> Roles { get; set; }
        public bool IsBanned { get; set; }
    }
}
