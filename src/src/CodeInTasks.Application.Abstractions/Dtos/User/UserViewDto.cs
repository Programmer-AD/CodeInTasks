namespace CodeInTasks.Application.Abstractions.Dtos.User
{
    //TODO: Find way to remove it
    public class UserViewDto
    {
        public string Name { get; set; }
        public IEnumerable<RoleEnum> Roles { get; set; }
        public bool IsBanned { get; set; }
    }
}
