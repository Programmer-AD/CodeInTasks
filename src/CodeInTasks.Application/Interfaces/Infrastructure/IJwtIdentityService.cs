using CodeInTasks.Application.Dtos.User;

namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface IJwtIdentityService
    {
        Task CreateUserAsync(UserCreateDto userDto);
        Task SetRole(string username, string roleName, bool isHave);
        Task SetBan(string username, bool isBanned);

        Task<UserViewDto> GetUserInfo(string username);
        Task<bool> TrySignInAsync(string username, string password, out string token);
    }
}
