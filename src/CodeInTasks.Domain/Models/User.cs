using Microsoft.AspNetCore.Identity;

namespace CodeInTasks.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public bool IsBanned { get; set; } = false;
    }
}
