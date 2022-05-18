namespace CodeInTasks.Application.Dtos.User
{
    public class UserViewDto
    {
        public string Name { get; set; }
        public IEnumerable<RoleEnum> Roles { get; set; }
        public bool IsBanned { get; set; }
    }
}
