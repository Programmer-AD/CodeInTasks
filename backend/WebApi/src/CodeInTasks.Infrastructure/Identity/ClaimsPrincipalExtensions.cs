using System.Security.Claims;

namespace CodeInTasks.Infrastructure.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdString = principal.FindFirstValue(IdentityConstants.UserIdClaimType);
            
            var userId = Guid.Parse(userIdString);
            return userId;
        }
    }
}
