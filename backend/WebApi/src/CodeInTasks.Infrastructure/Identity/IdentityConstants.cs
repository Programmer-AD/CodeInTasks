namespace CodeInTasks.Infrastructure.Identity
{
    internal static class IdentityConstants
    {
        public const string UserIdClaimType = "UserId";

        public static readonly TimeSpan TokenExpirationTime = TimeSpan.FromHours(1);
    }
}
