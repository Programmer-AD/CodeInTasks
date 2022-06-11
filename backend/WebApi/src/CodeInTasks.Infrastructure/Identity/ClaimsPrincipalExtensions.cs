using System.Security.Claims;

namespace CodeInTasks.Infrastructure.Identity
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdString = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = Guid.Parse(userIdString);
            return userId;
        }
    }
}
