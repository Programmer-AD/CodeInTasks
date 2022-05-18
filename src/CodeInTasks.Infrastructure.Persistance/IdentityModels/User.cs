using Microsoft.AspNetCore.Identity;

namespace CodeInTasks.Infrastructure.Persistance.IdentityModels
{
    public class User : IdentityUser<Guid>
    {
        public UserData UserData { get; set; }
    }
}
