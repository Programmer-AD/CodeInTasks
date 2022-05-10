using CodeInTasks.Application.Dtos.User;

namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface IJwtIdentityService
    {
        Task CreateUserAsync(UserCreateDto userDto);
        Task SetRole(Guid userId, string roleName, bool isHave);
        Task SetBanned(Guid userId, bool isBanned);

        Task<UserViewDto> GetUserInfo(Guid userId);
        Task<bool> TrySignInAsync(string username, string password, out string token);
    }
}
