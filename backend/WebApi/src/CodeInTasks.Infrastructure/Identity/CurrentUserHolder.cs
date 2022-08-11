using System.Security.Claims;

namespace CodeInTasks.Infrastructure.Identity
{
    public class CurrentUserHolder : ICurrentUserHolder
    {
        public ClaimsPrincipal Principal { get; private set; }

        public Guid? UserId => Principal?.GetUserId();

        public void Init(ClaimsPrincipal claimsPrincipal)
        {
            Principal = claimsPrincipal;
        }

        public bool IsInRole(string roleName)
        {
            return Principal.IsInRole(roleName);
        }
    }
}
