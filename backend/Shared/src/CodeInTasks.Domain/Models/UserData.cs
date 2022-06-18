namespace CodeInTasks.Domain.Models
{
    public class UserData : ModelBase
    {
        public string Name { get; set; }
        public IEnumerable<RoleEnum> Roles { get; set; }
        public bool IsBanned { get; set; }
    }
}
