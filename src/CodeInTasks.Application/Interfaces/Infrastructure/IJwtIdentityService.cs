using CodeInTasks.Application.Dtos.User;

namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface IJwtIdentityService
    {
        Task CreateUserAsync(UserCreateDto userDto);
        Task SetRoleAsync(Guid userId, string roleName, bool isHave);
        Task SetBanAsync(Guid userId, bool isBanned);

        Task<UserViewDto> GetUserInfoAsync(Guid userId);
        Task<bool> TrySignInAsync(string username, string password, out string token);
    }
}
