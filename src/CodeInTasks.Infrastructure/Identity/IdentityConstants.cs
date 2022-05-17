namespace CodeInTasks.Infrastructure.Identity
{
    public static class IdentityConstants
    {
        public const string UserIdClaimType = "UserId";

        public const int Password_RequiredLength = 8;

        public static readonly TimeSpan TokenExpirationTime = TimeSpan.FromHours(1);
    }
}
