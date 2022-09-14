using System.Security.Claims;

namespace CodeInTasks.Infrastructure.Identity
{
    public class CurrentUserHolder : ICurrentUserHolder
    {
        private ClaimsPrincipal principal;

        public Guid? UserId => principal?.GetUserId();

        public void Init(ClaimsPrincipal claimsPrincipal)
        {
            principal = claimsPrincipal;
        }

        public bool IsInRole(string roleName)
        {
            return principal.IsInRole(roleName);
        }
    }
}
