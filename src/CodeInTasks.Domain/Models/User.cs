using Microsoft.AspNetCore.Identity;

namespace CodeInTasks.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public bool IsBanned { get; set; } = false;
    }
}
