namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface IJwtIdentityService
    {
        Task<bool> TrySignInAsync(string username, string password, out string token);
        Task SetRole(Guid userId, string roleName, bool isHave);
        Task SetBanned(Guid userId, bool isBanned);
    }
}
